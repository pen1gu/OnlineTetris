using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace ChatServer
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var port = 52217;

            var socketHandler = new SocketHandler();
            await socketHandler.Run(port);
            return 0;
        }
    }
}
