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
        public Socket Socket { get; }

        public UserSocketData(string userName, Socket socket)
        {
            UserName = userName;
            Socket = socket;
        }
    }

    public partial class ChattingServer
    {
        private List<UserSocketData> clients = new List<UserSocketData>();

        public async Task Run(int port)
        {
            await StartListening(port);
        }

        private async Task StartListening(int port)
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    var socket = await listener.AcceptAsync();

                    HandleConnection(socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private async void HandleConnection(Socket client)
        {
            var buffer = new byte[1024];
            try
            {
                while (true)
                {
                    var (receiveCount, receiveText) = await client.ReceiveTextAsync(buffer);

                    if (receiveCount == 0)
                    {
                        Console.WriteLine("receive 0.");
                        //client.Close();
                        //if (clients.Any(x => x.Socket == client))
                        //{
                        //    clients = clients.Where(x => x.Socket != client).ToList();
                        //}
                        continue;
                        //return;
                    }

                    var obj = JObject.Parse(receiveText);
                    if (!obj.ContainsKey("Type"))
                    {
                        await client.SendDataAsync(new SC_System
                        {
                            Data = "올바르지 않은 패킷입니다.",
                        });
                    }

                    await HandlePacketAsync(client, obj);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                clients = clients.Where(x => x.Socket != client).ToList();
            }
            catch
            {
                clients = clients.Where(x => x.Socket != client).ToList();
            }
        }

        private async Task HandlePacketAsync(Socket clientSocket, JObject packetObj)
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
