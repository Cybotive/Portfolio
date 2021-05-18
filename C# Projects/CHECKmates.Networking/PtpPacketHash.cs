using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CHECKmates.Networking
{
    public class PtpPacketHash
    {
        // Must be public for JSON conversion
        private string packetId;
        public string PacketId
        {
            get { return packetId; }
            set { packetId = value; }
        }

        private SHA256 hash;
        public SHA256 Sha256
        {
            get { return hash; }
            set { hash = value; }
        }

        public PtpPacketHash(PtpPacket packet)
        {
            if(packet == null)
            {
                throw new ArgumentNullException("Packet may not be null.");
            }

            //packetId = packet.Id;
            hash = GetHash(packet);
        }

        private SHA256 GetHash(PtpPacket packet)
        {
            byte[] packetBytes = System.Text.Encoding.ASCII.GetBytes(PacketSerializer.SerializePtpPacket(packet));

            SHA256 sha = SHA256.Create();
            sha.ComputeHash(packetBytes);

            return sha;
        }
    }
}
