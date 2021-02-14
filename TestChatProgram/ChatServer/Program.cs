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
        static List<Socket> clients = new List<Socket>();

        public static async Task StartListening()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 52217);

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

                    clients.Add(socket);

                    HandleConnection(socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static async void HandleConnection(Socket client)
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
                        client.Close();
                        if (clients.Contains(client))
                        {
                            clients.Remove(client);
                        }
                        return;
                    }

                    Console.WriteLine($"Send message {receiveText} to {clients.Count} clients");
                    foreach (var clientSocket in clients)
                    {
                        if (clientSocket?.Connected ?? false)
                        {
                            Console.WriteLine($"Send: {receiveText}");
                            await clientSocket.SendTextAsync(receiveText);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                clients.Remove(client);
            }
            catch
            {
                clients.Remove(client);
            }
        }

        public static async Task<int> Main(string[] args)
        {
            await StartListening();
            return 0;
        }
    }
}
