using Common;
using Newtonsoft.Json.Linq;
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

namespace ChatClient_jkw
{
    public partial class MainClientForm : Form
    {
        SocketEx connection = null;
        public MainClientForm()
        {
            InitializeComponent();
        }

        private void ConnectServerButton_Click(object sender, EventArgs e)
        {
            Connect(IPAddress.Loopback); // ip 주소 연결
        }

        private async void Connect(IPAddress ip) // 이 작업 끝나기 전까지는 다른 작업 안함.
        {
            if (connection?.Connected ?? false)
            {
                var result = MessageBox.Show("Already connected. Reconnect?", "", MessageBoxButtons.YesNo);// 재 연결 하시겠습니까?
                if (result == DialogResult.No)
                {
                    return;
                }

                connection.Disconnect(false);
                connection.Close();
                connection = null;
            }
            //IPAddress ipAddress = IPAddress.Parse("221.143.21.37");
            IPEndPoint remoteEP = new IPEndPoint(ip, 52217);

            // Create a TCP/IP socket.  
            try
            {
                Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                await server.ConnectAsync(remoteEP);

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
        }

        private async void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var connected = connection?.Connected ?? false;
                if (!connected)
                {
                    MessageBox.Show("연결 먼저 하세요");
                    return;
                }

                await connection
                    .SendMessageAsync(new CS_Message
                    {
                        Text = richTextBox1.Text,
                    });
            }
        }

        private async Task HandleReceiveAsync() // 이 부분 질문. -> 
        {
            while (true)
            {
                var (receiveCount, receiveText) = await connection.ReceiveMessageAsync();
                if (receiveCount == 0)
                {
                    if (connection.Connected)
                    {
                        richTextBox1.Text += "receive 0 but connected. \n";
                    }
                    else
                    {
                        richTextBox1.Text += "receive 0 and disconnected. \n";
                    }
                    break;
                }

                richTextBox1.Text += receiveText + "\n";

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
                //Handle_SC_LoginAllow(packet);
            }
            else if (packetType == PacketType.SC_Message)
            {
                var packet = packetObj.ToObject<SC_Message>();

                richTextBox1.Text += $"[{packet.UserName}] {packet.Message}\n";
            }
        }


        private void ConnectRemoteServerButton_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("221.143.21.37");
            Connect(ipAddress);
        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
