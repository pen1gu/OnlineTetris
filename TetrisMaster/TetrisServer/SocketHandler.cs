using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TetrisServer
{
    public class SocketHandler
    {
        private Lobby _lobby;
        public async Task Run(int port)
        {
            _lobby = new Lobby();
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

                    HandleConnection(new SocketEx(socket));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private async void HandleConnection(SocketEx client)
        {
            IServerApi api = new ServerApi(_lobby, client);
            try
            {
                while (true)
                {
                    var (receiveCount, receiveText) = await client.ReceiveMessageAsync();

                    if (receiveCount == 0)
                    {
                        Console.WriteLine("receive 0.");

                        await api.ConnectionClosed();
                        return;
                    }

                    var obj = JObject.Parse(receiveText);
                    if (!obj.ContainsKey("Type"))
                    {
                        //await client.SendMessageAsync(new SC_System
                        //{
                        //    Data = "올바르지 않은 패킷입니다.",
                        //});
                    }

                    await api.HandlePacketAsync(obj);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
            }
        }
    }
}
