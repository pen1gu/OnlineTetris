using System;
using Common;
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

    public class CS_Start : PacketBase
    {
        public CS_Start()
            : base(PacketType.CS_Start)
        {
        }
    }

    public class CS_GetNextPiece : PacketBase
    {
        public CS_GetNextPiece()
            : base(PacketType.CS_GetNextPiece)
        {
        }
    }

    public class CS_Board : PacketBase
    {
        public BoardBase Board { get; set; }
        public CS_Board()
            : base(PacketType.CS_Board)
        {
        }
    }
}
