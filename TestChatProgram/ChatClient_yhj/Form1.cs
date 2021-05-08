using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient_yhj { 
    public partial class Form1 : Form
    {

        static Socket _connected;
        String _DisConnected = "연결된 서버가 없습니다.";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Connect(IPAddress ip) // TODO: 연결 로직 구성
        {
            _connected = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var iep = new IPEndPoint(ip,52217);

            _connected.Connect(iep);
            richTextBox1.AppendText("Connected...");

            // packet 설정(질문)
            _connected.Close();//소켓 

           try
            {
                Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                await server.ConnectAsync(iep);

                //richTextBox1.Text += $"Connected: {server.Connected} \n";

                connection = new SocketEx(server);

                await connection.SendMessageAsync(new CS_Login
                {
                    UserName = "현준",
                });

                await HandleReceiveAsync();
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "Error: " + ex.Message;
                connection?.Close();
                connection = null;
            }

            //센세 코드
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Connect(IPAddress.Loopback);
        }

        private void TxtCotents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (LbConnected.Text.Equals(_DisConnected))
                {
                    richTextBox1.AppendText(_DisConnected);
                    return;
                }

            SendMessage(TxtCotents.Text);
            }
        }

        private void SendMessage(String message)
        {
            richTextBox1.AppendText(message);

            object packet = null; // 임시 packet
            
            var sendBytes = Encoding.UTF8.GetBytes(message);
            _connected.Send(sendBytes);
            // message 
        }

        private void BtnDisConnect_Click(object sender, EventArgs e)
        {
            _connected.Disconnect(true);
        }
        // TODO: 연결, 채팅, 비연결 확인 로그, 로컬 연결, 서버 연결

        // TODO: 20210501 과제 : 로컬 연결
    }
}
