using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ServerPacketBase : PacketBase
    {
        public DateTime Time { get; } = DateTime.Now;

        public ServerPacketBase(PacketType type)
            : base(type)
        {
        }
    }
    public class SC_System : ServerPacketBase
    {
        public string Data { get; set; }

        public SC_System()
            : base(PacketType.SC_System)
        {
        }
    }

    public class SC_LoginAllow : ServerPacketBase
    {
        public bool Allow { get; set; }
        public string DeniedReason { get; set; }

        public SC_LoginAllow()
            : base(PacketType.SC_LoginAllow)
        {
        }
    }

    public class SC_Message : ServerPacketBase
    {
        public string UserName { get; set; }
        public string Message { get; set; }

        public SC_Message()
            : base(PacketType.SC_Message)
        {
        }
    }
}
