using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{

    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }

    class Program
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        static List<Socket> clients = new List<Socket>();

        public static async Task StartListening()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 52217);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    //allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");

                    var socket = await listener.AcceptAsync();

                    clients.Add(socket);

                    HandleConnection(socket);

                    //listener.BeginAccept(
                    //    new AsyncCallback(AcceptCallback),
                    //    listener);

                    // Wait until a connection is made before continuing.  
                    //allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        private static async void HandleConnection(Socket client)
        {
            var buffer = new byte[1024];
            try
            {
                while (true)
                {
                    var receiveCount = await client.ReceiveAsync(buffer, SocketFlags.None);

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

                    var message = Encoding.UTF8.GetString(buffer, 0, receiveCount);
                    var bytes = Encoding.UTF8.GetBytes(message);

                    Console.WriteLine($"Send message {message} to {clients.Count} clients");
                    foreach (var clientSocket in clients)
                    {
                        if (clientSocket?.Connected ?? false)
                        {
                            Console.WriteLine($"Send: {message}");
                            await clientSocket.SendAsync(bytes, SocketFlags.None);
                        }
                    }
                    //var tasks = clients.Select(async clientSocket =>
                    //{
                    //    Console.WriteLine($"Send({clientSocket.AddressFamily}): {message}");
                    //    await clientSocket.SendAsync(Encoding.UTF8.GetBytes(message), SocketFlags.None);
                    //});

                    //await Task.WhenAll(tasks);
                }
            }
            catch (SocketException)
            {
                clients.Remove(client);
            }
            catch
            {
                clients.Remove(client);
            }
        }
        public static async Task<int> Main(String[] args)
        {
            await StartListening();
            return 0;
        }
    }
}
