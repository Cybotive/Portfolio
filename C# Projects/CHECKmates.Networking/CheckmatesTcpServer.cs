using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CHECKmates.Networking
{
    public class CheckmatesTcpServer
    {
        public readonly static Int32 DefaultPort = 50020; // Default server port
        public readonly static int MaxPortAttempts = 3; // Maximum attempts at finding an unused port
        public readonly static Int32 PortInvalid = -1; // Code for invalid port (real ports can't be negative)

        private readonly static int MaxBacklog = 0; // Maximum clients to queue

        public readonly static int MaxPackets = 100; // Maximum packets to store simultaneously

        private TcpListener tcpListener;
        private List<Thread> clientThreads;
        private Dictionary<TcpClient, int> tcpClientDict;
        private Thread serverThread;
        private Thread responderThread;
        private string ipAndPort;
        private bool serverActive;
        private bool openToConnections;

        private IPAddress ipAddress;
        public IPAddress IpAddress
        {
            get { return ipAddress; }
        }

        private Int32 port; // Updates dynamically in case of port being taken
        public Int32 Port
        {
            get { return port; }
        }

        private BlockingCollection<PtpPacket> receivedPaksThreadSafe;
        public BlockingCollection<PtpPacket> ReceivedPackets
        {
            get { return receivedPaksThreadSafe; }
        }

        private BlockingCollection<PtpPacket> sentPaksThreadSafe;
        public BlockingCollection<PtpPacket> SentPackets
        {
            get { return sentPaksThreadSafe; }
        }

        /// <summary>
        /// Server for CHECKmates app-wide usage. TCP protocol.
        /// </summary>
        public CheckmatesTcpServer()
        {
            clientThreads = new List<Thread>();
            tcpClientDict = new Dictionary<TcpClient, int>();
            receivedPaksThreadSafe = new BlockingCollection<PtpPacket>();
            sentPaksThreadSafe = new BlockingCollection<PtpPacket>();
            serverThread = null;
            responderThread = null;
            serverActive = false;
            openToConnections = false;
            port = PortInvalid;
            ipAndPort = null;
        }

        /// <summary>
        /// Checker method to enforce usage of "Dynamic" port range only
        /// </summary>
        /// <param name="port"></param>
        /// <returns>bool PortValid</returns>
        public static bool IsValidPort(Int32 port)
        {
            return port >= 49152 && port <= 65535; // "Dynamic" ports range
        }

        public bool IsActive()
        {
            return serverActive;
        }

        public bool SendPacket(PtpPacket packet)
        {
            if (!serverActive)
            {
                throw new InvalidOperationException("Server must be active. Current Status: Inactive.");
            }
            if (packet is null)
            {
                throw new ArgumentNullException("Packet may not be null.");
            }
            if (tcpClientDict.Count == 0)
            {
                //throw new InvalidOperationException("No clients to send to.");
                return false;
            }

            string jsonPacket = PacketSerializer.SerializePtpPacket(packet);
            byte[] bytesToSend = Encoding.ASCII.GetBytes(jsonPacket);

            foreach (TcpClient client in tcpClientDict.Keys)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(bytesToSend, 0, bytesToSend.Length);
                }
                catch (IOException)
                {
                    // Transmission Failure
                    return false;
                }
                catch (ObjectDisposedException)
                {
                    // Current client is being closed. Move on to others.
                }
                catch (InvalidOperationException)
                {
                    // Current client is being closed. Move on to others.
                }
            }

            // All live transmissions successful
            sentPaksThreadSafe.Add(packet);
            return true;
        }

        public bool SendPacket(PtpPacket packet, TcpClient client)
        {
            if (!serverActive)
            {
                throw new InvalidOperationException("Server must be active. Current Status: Inactive.");
            }
            if (packet is null)
            {
                throw new ArgumentNullException("Packet may not be null.");
            }
            if (tcpClientDict.Count == 0)
            {
                throw new InvalidOperationException("No clients to send to.");
            }

            string jsonPacket = PacketSerializer.SerializePtpPacket(packet);
            byte[] bytesToSend = Encoding.ASCII.GetBytes(jsonPacket);

            try
            {
                NetworkStream stream = client.GetStream();
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (IOException)
            {
                // Transmission Failure
                return false;
            }
            catch (ObjectDisposedException)
            {
                // Current client is being closed.
                return false;
            }
            catch (InvalidOperationException)
            {
                // Current client is being closed.
                return false;
            }

            // Transmission successful
            sentPaksThreadSafe.Add(packet);
            return true;
        }

        public void PauseConnections()
        {
            openToConnections = false;
        }

        public void ResumeConnections()
        {
            openToConnections = true;
        }

        /// <summary>
        /// Start server on Local Area Network (LAN).
        /// </summary>
        /// <returns>
        /// On Fail: null | On Success: string containing ip and port of running server separated by ':'.
        /// </returns>
        public string StartOnLan()
        {
            string localHostname = Dns.GetHostName();
            IPAddress[] addressArray = Dns.GetHostEntry(localHostname).AddressList;

            List<IPAddress> localAddList = new List<IPAddress>();

            IPAddress localIp = null;
            foreach(IPAddress ip in addressArray)
            {
                // Test that current ip is IPV4 and it is not the "localhost" address.
                if(ip.AddressFamily is AddressFamily.InterNetwork && !ip.Equals(IPAddress.Loopback))
                {
                    if(localIp is null) // Make sure a LAN interface hasn't already been found
                    {
                        localIp = ip;
                    }
                    /*else
                    {
                        // Multiple local addresses. Fail.
                        //TODO: Potentially add a way to remedy.
                        throw new NotSupportedException("Cannot determine main NIC. Currently only supports one local network interface at a time.");
                    }*/
                }
            }

            return Start(localIp);
        }

        /// <summary>
        /// Start server on Loopback (AKA localhost) for internal and/or unit-testing purposes.
        /// </summary>
        /// <returns>
        /// On Fail: null | On Success: string containing ip and port of running server separated by ':'.
        /// </returns>
        public string StartOnLocalhost()
        {
            return Start(IPAddress.Loopback);
        }

        private string Start(IPAddress ip)
        {
            if (serverActive)
            {
                return ipAndPort;
            }

            ipAddress = ip;

            // Start server thread here
            port = TcpListen();

            ipAndPort = string.Format("{0}:{1}", ipAddress, port);

            if (port != PortInvalid)
            {
                this.serverActive = true;
                return ipAndPort; // Valid address found
            }

            return null; // No open port found
        }

        private int TcpListen()
        {
            try
            {
                int foundPort = FindOpenPort(DefaultPort);

                if(foundPort != PortInvalid)
                {
                    serverThread = new Thread(new ThreadStart(TcpServerThread)) { Name = "CHECKmatesServerThread_" + foundPort };
                    serverThread.Start();

                    responderThread = new Thread(new ThreadStart(PingResponder)) { Name = "CHECKmatesServerResponderThread_" + foundPort };
                    responderThread.Start();
                }

                return foundPort;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                KillServer();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("\nTCP Listen Failed!");
                Console.WriteLine("Message :{0} ", e.Message);
                KillServer();
            }

            return PortInvalid;
        }

        private int FindOpenPort(int startingPort)
        {
            bool portFound = false;
            for (int i = 1; i <= MaxPortAttempts && !portFound; i++)
            {
                if (!IsValidPort(startingPort))
                {
                    break;
                }

                try
                {
                    tcpListener = new TcpListener(ipAddress, startingPort);

                    // Start listening for client requests.
                    tcpListener.Start(MaxBacklog);

                    // Start didn't throw exception, so port was successful
                    portFound = true;
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode.Equals(SocketError.AddressAlreadyInUse))
                    {
                        if (i == MaxPortAttempts)
                        {
                            return PortInvalid; // Cancel server
                        }

                        startingPort++; // Increase port number by 1 before trying again
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            if (!portFound)
            {
                startingPort = PortInvalid;
            }

            return startingPort;
        }

        private void TcpServerThread()
        {
            serverActive = true; // Turns false when KillServer() is called
            openToConnections = true;

            while (serverActive)
            {
                // Accept Clients
                TcpClient client = null;
                try
                {
                    if (tcpListener != null && tcpListener.Pending())
                    {
                        client = tcpListener.AcceptTcpClient();

                        if (openToConnections)
                        {
                            //client = tcpListener.AcceptTcpClient();

                            // Thread waits here until client is accepted

                            lock (tcpClientDict)
                            {
                                lock (clientThreads)
                                {
                                    if (client != null)
                                    {
                                        Thread clientThread = new Thread(new ParameterizedThreadStart(TcpClientThread))
                                        {
                                            Name = "CHECKmatesClientThread_" + Port + "-" + clientThreads.Count
                                        };
                                        clientThread.Start(client);

                                        clientThreads.Add(clientThread);
                                        tcpClientDict.Add(client, clientThread.ManagedThreadId);
                                    }
                                }
                            }
                        }
                        else
                        {
                            DenyClient(client);
                        }
                        
                    }
                    
                }
                catch (SocketException)
                {
                    // Client failed but we can let while loop continue
                    KillClient(client);
                }
                catch (IOException)
                {
                    // Client failed but we can let while loop continue
                    KillClient(client);
                }
                catch (NullReferenceException e)
                {
                    if (serverActive == false)
                    {
                        // Server was killed on another thread while in while loop.
                    }
                    else
                    {
                        throw e;
                    }
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
            }
        }

        private void DenyClient(TcpClient client)
        {
            if(client != null)
            {
                bool canConnect = false;
                Ping_Packet denialPingPak = new Ping_Packet(canConnect);
                SendPacket(denialPingPak, client);

                KillClient(client);
            }
        }

        private void TcpClientThread(object obj)
        {
            TcpClient client;

            if (obj is TcpClient)
            {
                client = obj as TcpClient;
            }
            else
            {
                return;
            }

            try
            {
                NetworkStream stream = client.GetStream();

                while (serverActive && tcpClientDict.ContainsKey(client) && receivedPaksThreadSafe.Count < MaxPackets)
                {
                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];

                    string receivedData = null;
                    PtpPacket receivedPak = null;

                    int i;
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to an ASCII string.
                        receivedData = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", receivedData);
                    }

                    if (receivedData != null) //Might need to check for merged packets
                    {
                        receivedPak = PacketSerializer.DeserializePtpPacket(receivedData);
                        //receivedPackets.Enqueue(receivedPak);
                        receivedPaksThreadSafe.Add(receivedPak);
                    }
                }
            }
            catch (SocketException)
            {
                KillClient(client);
            }
            catch (IOException)
            {
                KillClient(client);
            }
            catch (ObjectDisposedException)
            {
                KillClient(client);
            }
            catch (InvalidOperationException)
            {
                KillClient(client);
            }
            catch (ThreadInterruptedException)
            {
                KillClient(client);

                return;
            }
            
        }

        private void PingResponder()
        {
            try
            {
                while (serverActive)
                {
                    if (ReceivedPackets.Count > 0)
                    {
                        PtpPacket curPacket;

                        if (ReceivedPackets.TryTake(out curPacket))
                        {
                            PtpPacket outgoingPak = null;

                            switch (curPacket.packetType)
                            {
                                case "Ping_Packet":
                                    Ping_Packet outgoingPing = curPacket as Ping_Packet;

                                    // Swap IDs before sending out same packet
                                    string tempSwap = outgoingPing.destIdCode;
                                    outgoingPing.destIdCode = outgoingPing.originIdCode;
                                    outgoingPing.originIdCode = tempSwap;

                                    // Let Client know if this server is at a point to accept new clients
                                    outgoingPing.acceptingConnections = openToConnections;

                                    // End the ping responses
                                    outgoingPing.needsResponse = false;

                                    // Translate packet back to be sent
                                    outgoingPak = outgoingPing;

                                    break;
                            }

                            if (outgoingPak != null)
                            {
                                if (curPacket.needsResponse)
                                {
                                    SendPacket(outgoingPak);
                                }
                            }
                            else
                            {
                                if (curPacket.needsResponse)
                                {
                                    // Re-add packet to collection if we didn't respond
                                    ReceivedPackets.Add(curPacket);
                                }
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // Collection closed
            }
        }

        public List<PtpPacket> GetReceivedPacketsByOrigin(string originIdCode)
        {
            PtpPacket[] allPackets = ReceivedPackets.ToArray();
            List<PtpPacket> matchingPackets = new List<PtpPacket>();

            foreach(PtpPacket packet in allPackets)
            {
                if (packet.originIdCode.Equals(originIdCode))
                {
                    matchingPackets.Add(packet);
                }
            }

            return matchingPackets;
        }

        public List<PtpPacket> GetSentPacketsByDest(string destinationIdCode)
        {
            PtpPacket[] allPackets = SentPackets.ToArray();
            List<PtpPacket> matchingPackets = new List<PtpPacket>();

            foreach (PtpPacket packet in allPackets)
            {
                // May want to remove the ability for null
                if ((packet.destIdCode == null && destinationIdCode == null) || packet.destIdCode.Equals(destinationIdCode))
                {
                    matchingPackets.Add(packet);
                }
            }

            return matchingPackets;
        }

        private void KillClient(TcpClient client)
        {
            if (client is null)
                return;

            client.Close();

            KeyValuePair<TcpClient, int> clientIdPair = tcpClientDict.SingleOrDefault(keyValue => keyValue.Key.Equals(client));
            Thread associatedThread = clientThreads.Find(thread => thread.ManagedThreadId == clientIdPair.Value);

            if (associatedThread != null)
            {
                lock (associatedThread)
                {
                    try
                    {
                        associatedThread.Abort();
                    }
                    catch (NotSupportedException)
                    {
                        // Abort not supported on current platform, continue to garbage collection (likely only on PC).
                    }

                    lock (clientThreads)
                    {
                        clientThreads.Remove(associatedThread);
                    }
                }
            }

            tcpClientDict.Remove(client);
        }

        // Non-Event version of KillServer
        public void KillServer()
        {
            KillServer(null, null);
        }

        public void KillServer(object sender, EventArgs e)
        {
            // Stop server
            openToConnections = false;
            serverActive = false;

            if (tcpListener != null)
            {
                lock (tcpListener)
                {
                    tcpListener.Stop();
                    tcpListener = null;
                }
            }

            if (responderThread != null)
            {
                lock (responderThread)
                {
                    try
                    {
                        responderThread.Abort(); // Now redundant since serverActive lets thread fall out of loop
                    }
                    catch (NotSupportedException)
                    {
                        // Abort not supported on current platform, continue to null assignment
                    }

                    // Last resort to kill thread
                    responderThread = null;
                }
            }

            if (serverThread != null)
            {
                lock (serverThread)
                {
                    try
                    {
                        serverThread.Abort(); // Now redundant since serverActive lets thread fall out of loop
                    }
                    catch (NotSupportedException)
                    {
                        // Abort not supported on current platform, continue to null assignment
                    }

                    // Last resort to kill thread
                    serverThread = null;
                }
            }

            if (tcpClientDict != null)
            {
                // Close and remove client objects
                lock (tcpClientDict)
                {
                    foreach (KeyValuePair<TcpClient, int> clientIdPair in tcpClientDict)
                    {
                        //clientIdPair.Key.Close();
                        KillClient(clientIdPair.Key);
                    }

                    tcpClientDict.Clear();
                }
            }

            // Clear pending packets
            if(sentPaksThreadSafe != null)
            {
                lock (sentPaksThreadSafe)
                {
                    sentPaksThreadSafe = new BlockingCollection<PtpPacket>();
                }
            }

            if (receivedPaksThreadSafe != null)
            {
                lock (receivedPaksThreadSafe)
                {
                    receivedPaksThreadSafe = new BlockingCollection<PtpPacket>();
                }
            }

            // TODO: Report to user
            //tcpStatusLabel.Text = serverStopMsg;
        }
    }
}
