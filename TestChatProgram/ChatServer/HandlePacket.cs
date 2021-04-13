using Common;
using JkwExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public partial class ChattingServer
    {
        private async Task Handle_CS_Login(Socket clientSocket, CS_Login packet)
        {
            clients.Add(new UserSocketData(packet.UserName, clientSocket));

            await clientSocket.SendDataAsync(new SC_LoginAllow
            {
                Allow = true,
            });

            await clients.Where(x => x.Socket.Connected)
                .ForEachParallelAsync(async x =>
                {
                    await x.Socket.SendDataAsync(new SC_System
                    {
                        Data = $"{packet.UserName}님이 로그인했습니다.",
                    });
                });
        }

        private async Task Handle_CS_Message(UserSocketData userSocketData, CS_Message packet)
        {
            await clients.Where(x => x.Socket.Connected)
                .ForEachParallelAsync(async x =>
                {
                    await x.Socket.SendDataAsync(new SC_Message
                    {
                        UserName = userSocketData.UserName,
                        Message = packet.Text,
                    });
                });
        }
    }
}
