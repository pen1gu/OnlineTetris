using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CS_Login : PacketBase
    {
        public string UserName { get; set; }

        public CS_Login()
            : base(PacketType.CS_Login)
        {
        }
    }

    public class CS_Message : PacketBase
    {
        public string Text { get; set; }

        public CS_Message()
            : base(PacketType.CS_Message)
        {
        }
    }
}
