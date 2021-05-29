using CHECKmates.Networking;
using NUnit.Framework;
using System;

namespace CHECKmates.Tests
{
    public class JoiningLobbyTest
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
        }

        /*[Test]
        public void Client_Connecting_To_Lobby_With_LobbyCode()
        {
            // Assuming server created a lobby code, retrieve it
            //string lobbyCode = localServer.LobbyCode; // Not Implemented.

            // Connect client to lobby
            bool success = false;
            //bool success = localClient.ConnectLobby(lobbyCode); // Not Implemented.

            // Assert client found and connected to lobby with known LobbyCode
            Assert.IsTrue(success);
        }*/

        [OneTimeTearDown]
        public void FreeResources()
        {
            localClient.Close();
            localServer.KillServer();
        }
    }
}