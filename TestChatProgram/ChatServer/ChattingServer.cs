using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class UserSocketData
    {
        public string UserName { get; } 
        public SocketEx Socket { get; }

        public UserSocketData(string userName, SocketEx socket)
        {
            UserName = userName;
            Socket = socket;
        }
    }

    public partial class ChattingServer // 연결해야할 채팅 서버 
    {
        private List<UserSocketData> clients = new List<UserSocketData>();

        public Task DisconnectAsync(SocketEx client)
        {
            clients = clients.Where(x => x.Socket != client).ToList();

            return Task.CompletedTask;
        }

        public async Task HandlePacketAsync(SocketEx clientSocket, JObject packetObj)
        {
            var packetType = Enum.Parse<PacketType>(packetObj.Value<string>("Type"));
            var userData = clients.FirstOrDefault(x => x.Socket == clientSocket);

            if (packetType == PacketType.CS_Login)
            {
                var packet = packetObj.ToObject<CS_Login>();
                await Handle_CS_Login(clientSocket, packet);
            }
            else if (packetType == PacketType.CS_Message)
            {
                var packet = packetObj.ToObject<CS_Message>();
                await Handle_CS_Message(userData, packet);
            }
        }
    }
}
