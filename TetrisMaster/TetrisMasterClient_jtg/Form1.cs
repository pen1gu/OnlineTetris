using System;
using Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing.Imaging;

namespace TetrisMasterClient_jtg
{
    public partial class Form1 : Form
    {
        SocketEx connection = null;
        TetrisThread tetrisThread = new TetrisThread();

        static Bitmap image = (Bitmap) Image.FromFile("images\tiles.png");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect(IPAddress.Loopback);
        }

        private async void Connect(IPAddress ip)
        {
            if (connection?.Connected ?? false)
            {
                var result = MessageBox.Show("이미 서버에 연결되어 있습니다.");
                return;
            }

            IPEndPoint remoteEP = new IPEndPoint(ip, 52217);
 
            try
            {
                Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                await server.ConnectAsync(remoteEP);

                connection = new SocketEx(server);

                await connection.SendMessageAsync(new CS_Login
                {
                    UserName = "jtg",
                });

                await HandleReceiveAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"서버 연결에 실패하였습니다. {ex.Message}");
                connection?.Close();
                connection = null;
            }
        }

        private async Task HandleReceiveAsync()
        {
            while (true)
            {
                var (receiveCount, receiveText) = await connection.ReceiveMessageAsync();
                if (receiveCount == 0)
                {
                    if (connection.Connected)
                    {
                        
                    }
                    else
                    {
                        
                    }
                    break;
                }

                

                var obj = JObject.Parse(receiveText);
                if (!obj.ContainsKey("Type"))
                {
                    continue;
                }

                await HandlePacketAsync(obj);
            }
        }

        private async Task HandlePacketAsync(JObject packetObj)
        {
            var packetType = Enum.Parse<PacketType>(packetObj.Value<string>("Type"));

            if (packetType == PacketType.SC_LoginAllow)
            {
                var packet = packetObj.ToObject<SC_LoginAllow>();
                Handle_SC_LoginAllow(packet);
            }
            else if (packetType == PacketType.SC_MemberUpdated)
            {
                var packet = packetObj.ToObject<SC_MemberUpdated>();

                listBox1.Items.Clear();

                foreach (var item in packet.UserList)
                {
                    listBox1.Items.Add(item);
                }
            }
            else if (packetType == PacketType.SC_NextPiece)
            {

            }
            else if (packetType == PacketType.SC_Start)
            {
                for (int i = 0; i < 6; i++)
                {
                    await NextPiece();
                }

                Thread Thread = new Thread(tetrisThread.Run);
                Thread.Start();
            }
            else if (packetType == PacketType.SC_BoardUpdated)
            {

            }
        }

        private void Handle_SC_LoginAllow(SC_LoginAllow packet)
        {
            if (packet.Allow)
            {
                MessageBox.Show($"서버 연결에 성공하였습니다!");
            }
            else
            {
                MessageBox.Show($"서버 연결에 실패하였습니다. {packet.DeniedReason}");
            }
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            var connected = connection?.Connected ?? false;

            if (!connected)
            {
                MessageBox.Show("서버 연결을 먼저 하세요");
                return;
            }

            await connection
                    .SendMessageAsync(new CS_Start{});
        }

        private async Task NextPiece()
        {
            await connection
                    .SendMessageAsync(new CS_GetNextPiece { });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tetrisThread.isEnded = true;
        }

        private void DrawBoard(Panel panel, BoardBase boardBase)
        {
            
            
        }

        private void PlayPanel_Paint(object sender, PaintEventArgs e)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        PieceType PieceType = boardBase.Board[i, j];
            //        Rectangle rect = new Rectangle((int)PieceType * 18, 0, 18, 18);
            //        var corpImage = image.Clone(rect, PixelFormat.DontCare);
                    
            //    }
            //}
        }
    }
}
