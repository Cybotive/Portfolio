using CHECKmates.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace CHECKmates.Networking
{
    public static class PacketSerializer
    {
        public static string SerializePtpPacket(PtpPacket packet)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(packet);
        }

        public static PtpPacket DeserializePtpPacket(string json)
        {
            PtpPacket partialPak = Newtonsoft.Json.JsonConvert.DeserializeObject<PtpPacket>(json);
            string pakType = partialPak.packetType;

            switch (pakType)
            {
                case "Friend_Add_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Friend_Add_Packet>(json);

                case "Friend_Invite_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Friend_Invite_Packet>(json);

                case "Host_Options_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Host_Options_Packet>(json);

                case "User_Ready_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<User_Ready_Packet>(json);

                case "User_Votes_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<User_Votes_Packet>(json);

                case "Ping_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Ping_Packet>(json);

                case "User_Disconnecting_Packet":
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<User_Disconnecting_Packet>(json);

                default:
                    throw new FormatException("Unknown Packet Type: " + pakType);
            }
        }

    }
}
