using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CHECKmates.Networking;
using System.Net;
using System.Threading;

namespace CHECKmates.Tests
{
    public class CheckmatesTcpClientTest
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

        [Test]
        public void Connect_Connects_With_Localhost_Server()
        {
            // Make client
            CheckmatesTcpClient client = new CheckmatesTcpClient();

            // Initiate connection
            Int32 port = client.Connect(localServer.IpAddress, localServer.Port);

            // Make sure client has a port and that connection to host was successful
            Assert.AreNotEqual(port, CheckmatesTcpServer.PortInvalid);
            Assert.IsTrue(client.IsConnectedAsync().Result);

            // Free resources
            client.Close();
        }

        [Test]
        public void Connect_Returns_Active_Port_On_Consecutive_Call()
        {
            // Ensure client has been connected previously
            Assert.IsTrue(localClient.IsConnectedAsync().Result);

            // Re-initiate connection
            Int32 port = localClient.Connect(localServer.IpAddress, localServer.Port);

            // Make sure client is connected correctly
            Assert.AreNotEqual(port, CheckmatesTcpServer.PortInvalid);
            Assert.IsTrue(localClient.IsConnectedAsync().Result);
        }

        [Test]
        public void Connect_Throws_Exception_On_Second_Connect_With_New_IP()
        {
            // Create IP unlikely to be used
            IPAddress newIp = IPAddress.Parse("2.3.4.5");

            // Ensure client has been connected previously
            Assert.IsTrue(localClient.IsConnectedAsync().Result);

            // Test that it throws
            Assert.Throws<InvalidOperationException>(
                delegate { localClient.Connect(newIp); }, 
                "Client is connected to another host.");
        }

        [Test]
        public void Close_Frees_Client_Resources()
        {
            // Make client
            CheckmatesTcpClient client = new CheckmatesTcpClient();

            // Initiate connection
            Int32 port = client.Connect(localServer.IpAddress, localServer.Port);

            // Make sure client has a port and that connection to host was successful
            Assert.AreNotEqual(port, CheckmatesTcpServer.PortInvalid);
            Assert.IsTrue(client.IsConnectedAsync().Result);

            // Free resources
            client.Close();

            // Make sure client closed
            Assert.IsFalse(client.IsConnectedAsync().Result);

            // Make a fresh client in place of disposed client
            client = new CheckmatesTcpClient();

            Thread.Sleep(500);

            // Initiate new connection
            Int32 portTwo = client.Connect(localServer.IpAddress, localServer.Port);

            // Assert both connections happened on same port. Proves resources were freed however has chance of failing.
            Assert.AreEqual(port, portTwo);

            // Free resources again
            client.Close();
        }

        [Test]
        public void SendPacket_Ping_Packet_Returns_True_With_Active_Connection()
        {
            // Build packet
            bool openToConnections = true;
            PtpPacket packet = new Ping_Packet(openToConnections);

            // Send packet
            bool success = localClient.SendPacketAsync(packet).Result;

            // Make sure sending packet was successful
            Assert.IsTrue(success);
        }

        [Test]
        public void SendPacket_Not_Ping_Packet_Returns_True_With_Active_Connection()
        {
            // Build packet
            VoterChoice[] voteList = new VoterChoice[]
            {
                new VoterChoice("Choice A."),
                new VoterChoice("Choice B.")
            };
            PtpPacket packet = new User_Votes_Packet(voteList);

            // Send packet
            bool success = localClient.SendPacketAsync(packet).Result;

            // Make sure sending packet was successful
            Assert.IsTrue(success);
        }

        [Test]
        public void SendPacket_Succeeds_In_Succession()
        {
            // Build packet
            VoterChoice[] voteList = new VoterChoice[]
            {
                new VoterChoice("Choice A."),
                new VoterChoice("Choice B.")
            };
            PtpPacket packet = new User_Votes_Packet(voteList);

            // Send packet
            bool success = localClient.SendPacketAsync(packet).Result;

            // Send packet again
            bool successTwo = localClient.SendPacketAsync(packet).Result;

            // Make sure sending packet was successful
            Assert.IsTrue(success);
            Assert.IsTrue(successTwo);
        }

        [Test]
        public void IsConnected_Returns_Correct_Status()
        {
            // Create temp server for closing
            CheckmatesTcpServer tempServer = new CheckmatesTcpServer();

            // Start server and Assert it has
            Assert.IsNotNull(tempServer.StartOnLocalhost());

            // Make client
            CheckmatesTcpClient client = new CheckmatesTcpClient();

            // Initiate connection
            Int32 port = client.Connect(tempServer.IpAddress, tempServer.Port);

            // Make sure client has a port
            Assert.AreNotEqual(port, CheckmatesTcpServer.PortInvalid);

            // Make sure client reports as connected
            Assert.IsTrue(client.IsConnectedAsync().Result);

            // Stop server
            tempServer.KillServer();

            // Make sure client reports as disconnected
            Assert.IsFalse(client.IsConnectedAsync().Result);

            // Free resources
            client.Close();
        }

        /*[Test]
        public void Connect_To_Active_Server_Sets_Connected_To_True()
        {
            while (!localClient.Connected)
            {
                Thread.Sleep(500);
            }
            
            Assert.True(localClient.Connected);
        }*/
        
        [OneTimeTearDown]
        public void StopServer()
        {
            localServer.KillServer();
        }
    }
}
