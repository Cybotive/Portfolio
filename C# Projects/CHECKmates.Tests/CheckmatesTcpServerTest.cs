using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CHECKmates.Networking;
using System.Net;
using System.Threading;

namespace CHECKmates.Tests
{
    public class CheckmatesTcpServerTest
    {
        CheckmatesTcpServer localServer;
        CheckmatesTcpClient localClient;

        [OneTimeSetUp]
        public void StartServer()
        {
            localServer = new CheckmatesTcpServer();
            localClient = new CheckmatesTcpClient();

            // Start server and assert it has
            Assert.IsNotNull(localServer.StartOnLocalhost());

            // Create client and assert it connected
            Assert.AreNotEqual(CheckmatesTcpServer.PortInvalid, localClient.Connect(localServer.IpAddress, localServer.Port));
        }

        [OneTimeTearDown]
        public void FreeResources()
        {
            localClient.Close();
            localServer.KillServer();
        }

        /// <remarks>
        /// WARNING: May need Firewall access.
        /// Currently fails with multiple LAN NICs (shouldn't be an issue on any Android/iOS native device).
        /// </remarks>
        [Test]
        [Category("Firewall")]
        public void StartOnLan_Makes_Only_One_Server()
        {
            // Create server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            string address = null;

            try
            {
                // Start server
                address = server.StartOnLan();

                // Make sure server is started
                Assert.IsNotNull(address);

                // Make sure a second call to start doesn't make a new server
                Assert.AreEqual(server.StartOnLan(), address);
            }
            catch(NotSupportedException) // This should only "catch" on PCs. 
            {
                // Not a hard fail, but also not sure if method is fully functional.
                Assert.Inconclusive();
            }
            
            // Close server
            server.KillServer();
        }

        [Test]
        public void StartOnLocalhost_Makes_Only_One_Server()
        {
            // Create server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            string address = server.StartOnLocalhost();

            // Make sure server is started
            Assert.IsNotNull(address);

            // Make sure a second call to start doesn't make a new server
            Assert.AreEqual(server.StartOnLocalhost(), address);

            // Close server
            server.KillServer();
        }

        /// <summary>
        /// Asserts port collisions are handled.
        /// </summary>
        /// <remarks>
        /// Uses localhost server.
        /// </remarks>
        [Test]
        public void Finds_Unused_Port_During_Start()
        {
            // Create server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            string address = server.StartOnLocalhost();

            // Create another server
            CheckmatesTcpServer serverOther = new CheckmatesTcpServer();
            string addressOther = serverOther.StartOnLocalhost();

            // Make sure servers started
            Assert.IsNotNull(address);
            Assert.IsNotNull(addressOther);

            // Separate IP and port
            string[] addressSplit = address.Split(':');
            string[] addressSplitOther = addressOther.Split(':');

            // Make sure IP's are the same while ports are different
            Assert.AreEqual(addressSplit[0], addressSplitOther[0]);
            Assert.AreNotEqual(addressSplit[1], addressSplitOther[1]);

            // Close servers
            server.KillServer();
            serverOther.KillServer();
        }

        [Test]
        public void KillServer_Frees_Port()
        {
            // Create server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            string addressBefore = server.StartOnLocalhost();

            // Make sure server is started
            Assert.IsNotNull(addressBefore);

            // Close server (theoretically frees port)
            server.KillServer();

            // Start server (hopefully on same port as before)
            string addressAfter = server.StartOnLocalhost();

            // Make sure servers started on same IP:port combination
            Assert.AreEqual(addressBefore, addressAfter);

            // Close server
            server.KillServer();
        }

        [Test]
        public void SendPacket_Ping_Packet_Returns_True_With_Active_Connection()
        {
            PtpPacket packet = new Ping_Packet(true);

            // Send packet
            bool success = localServer.SendPacket(packet);

            // Make sure sending packet was successful
            Assert.IsTrue(success);
        }

        // Confirmed working, but can't test since server auto removes received packet after processing
        /*[Test]
        public void SendPacket_Ping_Packet_Receives_Same_Packet_With_IdCodes_Swapped()
        {
            var originId = "TempOriginCode";
            var destId = "TempDestinationCode";

            // Create throwaway server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            server.StartOnLocalhost();

            // Create throwaway client to let server not fail
            CheckmatesTcpClient client = new CheckmatesTcpClient();
            client.Connect(server.IpAddress, server.Port);

            // Pause so client and server threads have enough time to handshake
            Thread.Sleep(200);

            // Create ping packet to send
            PtpPacket packet = new Ping_Packet(true)
            {
                originIdCode = originId,
                destIdCode = destId
            };

            // Create expected packet
            PtpPacket expectedPacket = new Ping_Packet(true)
            {
                originIdCode = destId,
                destIdCode = originId
            };

            // Send out packet
            server.SendPacket(packet);

            // Assert packet sent
            List<PtpPacket> sentPackets = server.GetSentPacketsByDest(destId);
            Assert.IsTrue(sentPackets.Contains(packet));

            // Yield to server thread
            Thread.Sleep(500);

            // Assert client sent back correctly formatted packet
            List<PtpPacket> receivedPackets = server.GetReceivedPacketsByOrigin(destId);
            Assert.IsTrue(receivedPackets.Contains(expectedPacket));

            // Free Resources
            server.KillServer();
        }*/

        [Test]
        public void SendPacket_Not_Ping_Packet_Returns_True_With_Active_Connection()
        {
            VoterChoice[] voteList = new VoterChoice[]
            {
                new VoterChoice("Choice A."),
                new VoterChoice("Choice B.")
            };

            PtpPacket packet = new User_Votes_Packet(voteList)
            {
                //Id = "SendPacket_Returns_True_With_Active_Server Test",
            };

            // Send packet
            bool success = localServer.SendPacket(packet);

            // Make sure sending packet was successful
            Assert.IsTrue(success);
        }

        [Test]
        public void SendPacket_Succeeds_In_Succession()
        {
            PtpPacket packet = new Ping_Packet(true);

            // Send packet
            bool success = localServer.SendPacket(packet);

            // Send packet again
            bool successTwo = localServer.SendPacket(packet);

            // Make sure sending packet was successful
            Assert.IsTrue(success);
            Assert.IsTrue(successTwo);
        }

        [Test]
        public void Server_PauseConnections_Denies_Clients()
        {
            // Create clients for connecting
            CheckmatesTcpClient clientAllowed = new CheckmatesTcpClient();
            CheckmatesTcpClient clientDenied = new CheckmatesTcpClient();

            // Attempt client to server connection
            int portAllowed = clientAllowed.Connect(localServer.IpAddress, localServer.Port);

            // Pause the server from accepting new connections
            localServer.PauseConnections();

            // Attempt client to server connection
            int portDenied = clientDenied.Connect(localServer.IpAddress, localServer.Port);

            // Assert clients were allowed and denied as intended
            Assert.AreNotEqual(CheckmatesTcpServer.PortInvalid, portAllowed);
            Assert.AreEqual(CheckmatesTcpServer.PortInvalid, portDenied);

            // Maybe add resume to see what happens.
        }

        [Test]
        public void Server_ResumeConnections_Accepts_Clients()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Multiple_Clients_Rapidly_Connect()
        {
            // Create temp server
            CheckmatesTcpServer server = new CheckmatesTcpServer();
            server.StartOnLocalhost();

            // Create array with amount of clients to test
            CheckmatesTcpClient[] clients = new CheckmatesTcpClient[5];

            // Init array
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = new CheckmatesTcpClient();
            }

            foreach (CheckmatesTcpClient client in clients)
            {
                // Make sure all clients connect successfully
                if (client.Connect(server.IpAddress, server.Port) == CheckmatesTcpServer.PortInvalid)
                    Assert.Fail();
            }

            foreach (CheckmatesTcpClient client in clients)
            {
                // Free resources
                client.Close();
            }

            // Free resources
            server.KillServer();
        }
    }
}
