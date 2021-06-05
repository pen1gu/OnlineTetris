using System;
using Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace TetrisMasterClient_yhj
{
    public partial class Form1 : Form
    {

        SocketEx connection = null;
        User user;
        TetrisInfo info;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect(IPAddress ip)
        {
            if (connection?.Connected ?? false)
            {
                var result = MessageBox.Show("이미 서버에 연결되어 있습니다.");
                return;
            }

            IPEndPoint remoteEp = new IPEndPoint(ip, 52217);

            try {
                Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                await server.ConnectAsync(remoteEp);
                connection = new SocketEx(server);
                await connection.SendMessageAsync(new CS_Login
                {
                    UserName = "yhj"
                });

                await HandleReceiveAsync();
                user = new User("yhj");
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

                HandlePacketAsync(obj);
            }
        }

        private void HandlePacketAsync(JObject packetObj)
        {
            var packetType = Enum.Parse<PacketType>(packetObj.Value<string>("Type"));

            if (packetType == PacketType.SC_LoginAllow)
            {
                var packet = packetObj.ToObject<SC_LoginAllow>();
                Handle_SC_LoginAllow(packet);
            }

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Connect(IPAddress.Loopback);
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

        private async void GameStart()
        {
            var connected = connection?.Connected ?? false;

            if (!connected)
            {
                MessageBox.Show("서버 연결을 먼저 하세요");
                return;
            }

            await connection
                    .SendMessageAsync(new CS_Start { });

            
            MessageBox.Show("보드 연결");

            StartBoard();
        }

        private async void StartBoard()
        {
            info = new TetrisInfo(connection, user);
            await info.Run();
            UpdateBoard();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            GameStart();
        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        { 
            if (connection?.Connected ?? true)
            {
                return;
            }

            else if (e.KeyCode == Keys.Left)
            {
                info.MoveLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                info.MoveRight();
            }
            else if (e.KeyCode == Keys.Down)
            {
                info.MoveDown();
            }

            UpdateBoard();
        }

        private async void UpdateBoard()
        {
            // receive
        }
    }

}
