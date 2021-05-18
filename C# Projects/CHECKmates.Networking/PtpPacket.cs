using System;
using System.Collections.Generic;
using System.Text;

namespace CHECKmates.Networking
{
    public class PtpPacket
    {
        public string packetType;

        public string originIdCode;
        public string destIdCode;

        public bool needsResponse = false;
    }

    public class Friend_Add_Packet : PtpPacket
    {
        public string friendCode;

        public Friend_Add_Packet(string code)
        {
            base.packetType = "Friend_Add_Packet";

            this.friendCode = code;
        }
    }

    public class Friend_Invite_Packet : PtpPacket
    {
        public string inviteCode;

        public Friend_Invite_Packet(string code)
        {
            base.packetType = "Friend_Invite_Packet";

            this.inviteCode = code;
        }
    }

    public class Host_Options_Packet : PtpPacket
    {
        public string modeSelection;
        public string activitySelection;
        public int numChoiceSelection;
        public int numParticipantsSelection;
        public int timeLimitSelection;

        public Host_Options_Packet(string activity, string mode, int participants, int numchoices, int timelimit)
        {
            base.packetType = "Host_Options_Packet";

            activitySelection = activity;
            modeSelection = mode;
            numParticipantsSelection = participants;
            numChoiceSelection = numchoices;
            timeLimitSelection = timelimit;
        }

    }

    public class User_Ready_Packet : PtpPacket
    {
        public bool isReady;

        public User_Ready_Packet(bool isReady)
        {
            base.packetType = "User_Ready_Packet";

            this.isReady = isReady;
        }

    }

    public class User_Votes_Packet : PtpPacket
    {

        public VoterChoice[] voteList;

        public User_Votes_Packet(VoterChoice[] voteList)
        {
            base.packetType = "User_Votes_Packet";

            this.voteList = voteList;
        }

    }

    public class Ping_Packet : PtpPacket
    {

        public bool acceptingConnections;

        public Ping_Packet(bool acceptingConnections)
        {
            base.packetType = "Ping_Packet";
            base.needsResponse = true;

            this.acceptingConnections = acceptingConnections;
        }

    }

    public class User_Disconnecting_Packet : PtpPacket
    {

        public User_Disconnecting_Packet()
        {
            base.packetType = "User_Disconnecting_Packet";
        }

    }


}
