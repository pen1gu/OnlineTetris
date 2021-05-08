using System;
using System.Threading.Tasks;

namespace TetrisServer
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
