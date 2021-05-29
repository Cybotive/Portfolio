using CHECKmates.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHECKmates.Tests
{
    class PtpPacketHashTest
    {
        Friend_Add_Packet basePacket;
        string arbitraryFriendCode;

        // Initialize all global variables
        [OneTimeSetUp]
        public void Init()
        {
            arbitraryFriendCode = "25A";

            basePacket = new Friend_Add_Packet(arbitraryFriendCode);
        }

        [Test]
        public void PtpPacketHash_Creates_Same_Hash_Twice_With_Same_Packet_Data()
        {
            // Initialize two packets with same data
            string friendCodeA = "FDS3S";
            string friendCodeB = "FDS3S";

            Friend_Add_Packet packetThis = new Friend_Add_Packet(friendCodeA);
            Friend_Add_Packet packetThat = new Friend_Add_Packet(friendCodeB);

            // Hash both packets
            PtpPacketHash packetHashThis = new PtpPacketHash(packetThis);
            PtpPacketHash packetHashThat = new PtpPacketHash(packetThat);

            // Store hashes from packets
            var hashThis = packetHashThis.Sha256.Hash;
            var hashThat = packetHashThat.Sha256.Hash;

            // Make sure hash results are the same
            Assert.AreEqual(hashThis, hashThat);
        }

        [Test]
        public void PtpPacketHash_Creates_Different_Hash_With_Different_Packet_Data()
        {
            // Initialize two packets with different data
            string friendCodeA = "FDS3S";
            string friendCodeB = "AAAAA";

            Friend_Add_Packet packetThis = new Friend_Add_Packet(friendCodeA);
            Friend_Add_Packet packetThat = new Friend_Add_Packet(friendCodeB);

            // Hash both packets
            PtpPacketHash packetHashThis = new PtpPacketHash(packetThis);
            PtpPacketHash packetHashThat = new PtpPacketHash(packetThat);

            // Store hashes from packets
            var hashThis = packetHashThis.Sha256.Hash;
            var hashThat = packetHashThat.Sha256.Hash;

            // Make sure hash results are different
            Assert.AreNotEqual(hashThis, hashThat);
        }
    }
}
