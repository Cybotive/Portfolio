using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace CHECKmates.Networking
{
    public class CheckmatesTcpClient
    {
        private readonly int ConnectionTimeoutMs = 1000; // Time in ms to wait for Ping from Server during IsConnected() call

        private int defaultPort = CheckmatesTcpServer.DefaultPort;
        private int maxPort = CheckmatesTcpServer.DefaultPort + CheckmatesTcpServer.MaxPortAttempts - 1; // -1 since default is an attempt

        private TcpClient client;

        private IPAddress connectedIp;
        private Int32 connectedPort;

        private Thread clientSideThread;
        private bool active;
        private bool openToConnections;

        public bool Connected { get; private set; }

        private BlockingCollection<PtpPacket> sentPaksThreadSafe;
        public BlockingCollection<PtpPacket> SentPackets
        {
            get { return sentPaksThreadSafe; }
        }

        public IPAddress ConnectedIp
        {
            get { return connectedIp; }
        }
        public Int32 ConnectedPort
        {
            get { return connectedPort; }
        }

        public CheckmatesTcpClient()
        {
            client = null;
            connectedIp = null;
            connectedPort = CheckmatesTcpServer.PortInvalid;
            sentPaksThreadSafe = new BlockingCollection<PtpPacket>();
            clientSideThread = null;
            active = false;
            openToConnections = false;
            Connected = false;
        }

        /// <summary>
        /// Initiates connection to given IP address with assumptions on port.
        /// </summary>
        /// <param name="hostIp"></param>
        /// <returns>Int32 port - The port the client successfully connected on.</returns>
        public Int32 Connect(IPAddress hostIp)
        {
            if (hostIp is null)
            {
                return CheckmatesTcpServer.PortInvalid;
            }

            if (client != null && IsConnectedAsync().Result)
            {
                if (hostIp != connectedIp)
                {
                    throw new InvalidOperationException("Client is connected to another host.");
                }
                
                return connectedPort;
            }

            if (client == null)
            {
                client = new TcpClient();
            }

            Int32 port = defaultPort;

            while (client != null && !Connected)
            {
                if(port > maxPort)
                {
                    return CheckmatesTcpServer.PortInvalid;
                }

                Int32 returnedPort = CheckmatesTcpServer.PortInvalid;

                try
                {
                    returnedPort = Connect(hostIp, port);
                }
                catch(SocketException e)
                {
                    if(returnedPort != CheckmatesTcpServer.PortInvalid)
                    {
                        throw e;
                    }
                }

                if(returnedPort == CheckmatesTcpServer.PortInvalid)
                {
                    // Let the loop retry on another port
                    port++;
                }
                else
                {
                    Connected = true;
                }
                    
            }

            if(client == null)
            {
                return CheckmatesTcpServer.PortInvalid;
            }

            return port;
        }

        public Int32 Connect(IPAddress hostIp, Int32 port)
        {
            if (hostIp is null || port > maxPort || !CheckmatesTcpServer.IsValidPort(port))
            {
                return CheckmatesTcpServer.PortInvalid;
            }

            if (client != null && active)
            {
                if (hostIp != connectedIp || port != connectedPort)
                {
                    throw new InvalidOperationException("Client is connected to another server.");
                }

                return connectedPort;
            }

            if (client == null)
            {
                client = new TcpClient();
            }

            try
            {
                bool success = client.ConnectAsync(hostIp, port).Wait(ConnectionTimeoutMs);

                if (!success)
                {
                    //client.Close();
                    Close();
                    client = null;
                    return CheckmatesTcpServer.PortInvalid;
                }

                connectedIp = hostIp;
                connectedPort = port;
                active = true;
                openToConnections = true;
                Connected = true;

                clientSideThread = new Thread(new ThreadStart(ClientSideThreadAsync)) { Name = "CHECKmatesClientSideThread_" + port };
                clientSideThread.Start();


                //if (active && IsConnectedAsync().Result)
                if (active && Connected)
                {
                    return port;
                }
                else
                {
                    return CheckmatesTcpServer.PortInvalid;
                }
                
            }
            catch (SocketException)
            {
                Close();
                return CheckmatesTcpServer.PortInvalid;
            }
            catch (AggregateException e)
            {
                Close();
                return CheckmatesTcpServer.PortInvalid;
            }
        }

        public async Task<bool> SendPacketAsync(PtpPacket packet)
        {
            if (packet is null)
            {
                throw new ArgumentNullException("Packet may not be null.");
            }
            if (!client.Connected)
            {
                throw new InvalidOperationException("Client must be connected to a host.");
            }

            string jsonPacket = PacketSerializer.SerializePtpPacket(packet);
            byte[] bytesToSend = Encoding.ASCII.GetBytes(jsonPacket);

            NetworkStream stream = client.GetStream();

            try
            {
                await stream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
            }
            catch(IOException)
            {
                // Transmission Failure
                return false;
            }

            // Transmission Successful
            sentPaksThreadSafe.Add(packet);
            return true;
        }

        public List<PtpPacket> GetSentPacketsByDest(string destinationIdCode)
        {
            PtpPacket[] allPackets = SentPackets.ToArray();
            List<PtpPacket> matchingPackets = new List<PtpPacket>();

            foreach (PtpPacket packet in allPackets)
            {
                if (packet.destIdCode == null || packet.destIdCode.Equals(destinationIdCode))
                {
                    matchingPackets.Add(packet);
                }
            }

            return matchingPackets;
        }

        public async Task<bool> IsConnectedAsync()
        {
            try
            {
                /*
                SendPacket(new Ping_Packet(openToConnections)); // Send a ping to update status
                return SendPacket(new Ping_Packet(openToConnections)); // Send a ping to get a false kickback if status was closed above. Workaround of NetworkStream.
                */

                if(!active)
                {
                    return false;
                }

                Ping_Packet pingPak = new Ping_Packet(openToConnections);
                bool isSent = await SendPacketAsync(pingPak);

                if (isSent)
                {
                    return true;
                }

                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private async void ClientSideThreadAsync()
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (active)
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

                    if (receivedData != null) // Might need to check for merged packets
                    {
                        receivedPak = PacketSerializer.DeserializePtpPacket(receivedData);
                        Console.WriteLine(receivedPak);
                        
                        await ProcessPacket(receivedPak);
                    }
                }
            }
            catch (SocketException)
            {
                active = false;
                Close();
            }
            catch (IOException e)
            {
                active = false;
                Close();
            }
            catch (ObjectDisposedException)
            {
                active = false;
                Close();
            }
            catch (InvalidOperationException)
            {
                active = false;
                Close();
            }
            catch (ThreadInterruptedException)
            {
                Close();

                return;
            }

        }

        private async Task ProcessPacket(PtpPacket toProcessPak)
        {
            PtpPacket outgoingPak = null;

            // TODO verify DestId is this client

            switch (toProcessPak.packetType)
            {
                case "Ping_Packet":
                    Ping_Packet outgoingPing = toProcessPak as Ping_Packet;

                    if (!Connected && !outgoingPing.acceptingConnections)
                    {
                        // Server denied this client
                        active = false;
                        Connected = false;

                        Close();
                    }

                    Connected = true;

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

                case "Host_Options_Packet":
                    Host_Options_Packet hop = toProcessPak as Host_Options_Packet;

                    if (hop != null)
                    {
                        AppData.SetHostOptions(hop);
                        AppData.IsWaiting = false;
                    }

                    break;
            }

            if (outgoingPak != null && toProcessPak.needsResponse)
            {
                await SendPacketAsync(outgoingPak);
            }
            
        }

        public void Close()
        {
            Close(null, null);
        }

        public void Close(object sender, EventArgs e)
        {
            if (active)
            {
                SendDisconnection();
            }

            openToConnections = false;
            active = false;
            Connected = false;

            sentPaksThreadSafe.Dispose();

            clientSideThread = null;

            if(client != null)
            {
                client.Close();
            }

            client = null;
        }

        private async void SendDisconnection()
        {
            try
            {
                User_Disconnecting_Packet disPak = new User_Disconnecting_Packet();
                await SendPacketAsync(disPak);
            }
            catch (InvalidOperationException)
            {
                // Couldn't send disconnect
            }
        }
    }
}
