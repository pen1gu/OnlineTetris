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
using System.Threading;

namespace TetrisMasterClient_yhj
{
    public partial class Form1 : Form
    {

        SocketEx connection = null;
        User user;
        Board boardBase = new Board(5,10);
        bool checkPaint = false;
        /*public TetrisInfo info = new TetrisInfo();*/
        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

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

            try
            {
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

            checkPaint = true;
            Invalidate();
        }

        /*private  void StartBoard()
        { 
            startCheck = true;
            this.Refresh();
            UpdateBoard();
        }*/

        private void BtnStart_Click(object sender, EventArgs e)
        {
            GameStart();
            Graphics g;
            try
            {
                g = this.CreateGraphics();
            }
            catch
            {
                return;
            }

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j<12; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.White), 10 + j * 20, 10 + i * 20, 20, 20);
                    g.FillRectangle(Brushes.Black, 10 + j * 20, 10 + i * 20, 20, 20);
                }
            }


            //TODO: 뒷 배경 칠하기
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode + "");

            if (connection?.Connected ?? true)
            {
                MessageBox.Show("연결이 되어있지 않습니다.");
                return;
            }
            // send
            else if (e.KeyCode == Keys.Left)
            {
                semaphoreSlim.WaitAsync();
                MessageBox.Show("왼");
                boardBase.MoveLeft();
                semaphoreSlim.Release();
            }
            else if (e.KeyCode == Keys.Right)
            {
                semaphoreSlim.WaitAsync();
                MessageBox.Show("우");
                boardBase.MoveRight();
                semaphoreSlim.Release();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
            {
                semaphoreSlim.WaitAsync();
                MessageBox.Show("아래");
                boardBase.MoveDown();
                semaphoreSlim.Release();
            }

            UpdateBoard();
        }

        private async void UpdateBoard()
        {
            // receive
        }

        bool startCheck = false;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 움직임 테스트
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // 아무것도 없음
        }

        public void setLog(string log)
        {
            richTextBox1.AppendText(log);
        }

    
        public async Task<bool> MoveBlockDownLooplyAsync() // 동작 부분
        {
            while (true) // 계속해서 돌면서 delay 150을 넣음
            {
                await semaphoreSlim.WaitAsync();
                boardBase.MoveDown();

                boardBase.DrawBoard(this);

                /*if (IsGameOver())
                {
                    break;
                }*/

                await Task.Delay(150);
            }


            return true;
        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (checkPaint != false)
            {
                boardBase.NewBlock();
                await MoveBlockDownLooplyAsync();
                boardBase.DrawBoard((Form1)this);
                MessageBox.Show("Game Over.");
            }
        }
    }

}
