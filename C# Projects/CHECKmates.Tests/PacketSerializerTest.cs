using CHECKmates.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHECKmates.Tests
{
    class PacketSerializerTest
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
        public void Serialize_Returns_Valid_JSON_String()
        {
            string json = PacketSerializer.SerializePtpPacket(basePacket);
            string invalidJson = "NotJSON" + json;

            // Assert that JSON formatted data is received from method
            Assert.DoesNotThrow(delegate { JObject.Parse(json); });

            // Verify integrity of above test
            Assert.Throws<JsonReaderException>(delegate { JObject.Parse(invalidJson); });
            Assert.Throws<ArgumentNullException>(delegate { JObject.Parse(null); });
        }

        [Test]
        public void Deserialize_Returns_Original_Object()
        {
            // Get JSON version of packet. If Serialize test fails, this will fail as well.
            string json = PacketSerializer.SerializePtpPacket(basePacket);

            // Convert JSON back to PtpPacket
            PtpPacket deserializedPacket = PacketSerializer.DeserializePtpPacket(json);
            Friend_Add_Packet desVotesPak = deserializedPacket as Friend_Add_Packet;

            // Compare the objects based on the vote values
            bool equal = true;
            if (!basePacket.friendCode.Equals(desVotesPak.friendCode))
                equal = false;

            Assert.IsTrue(equal);
        }
    }
}
