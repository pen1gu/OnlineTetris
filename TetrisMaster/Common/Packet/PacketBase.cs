using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{

    public class PacketBase
    {
        public PacketType Type { get; private set; }

        public PacketBase(PacketType type)
        {
            Type = type;
        }
    }
}
