using System;
using Newtonsoft.Json.Linq;
using Common;
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

namespace ChatClient_jtg
{
    public partial class Form1 : Form
    {
        SocketEx connection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect(IPAddress ip)
        {
            if (connection?.Connected ?? false)
            {
                var result = MessageBox.Show("Already connected. Reconnect?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
                connection.Disconnect(false);
                connection.Close();
                connection = null;
            }

            IPEndPoint remoteEP = new IPEndPoint(ip, 52217);

            // Create a TCP/IP socket.  
            try
            {
                Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                await server.ConnectAsync(remoteEP);

                connection = new SocketEx(server);

                await connection.SendMessageAsync(new CS_Login
                {
                    UserName = "택규",
                });

                await HandleReceiveAsync();
            }
            catch (Exception ex)
            {
                ChattingListTextBox.Text += "Error: " + ex.Message;
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
                        ChattingListTextBox.Text += "receive 0 but connected. \n";
                    }
                    else
                    {
                        ChattingListTextBox.Text += "receive 0 and disconnected. \n";
                    }
                    break;
                }

                ChattingListTextBox.Text += receiveText + "\n";

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
            else if (packetType == PacketType.SC_Message)
            {
                var packet = packetObj.ToObject<SC_Message>();

                ChattingListTextBox.Text += $"[{packet.UserName}] {packet.Message}\n";
            }
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("확인");
            }
        }

        private void ConnetToServerBtn_Click(object sender, EventArgs e)
        {
            Connect(IPAddress.Loopback);
        }

        private void Handle_SC_LoginAllow(SC_LoginAllow packet)
        {
            if (packet.Allow)
            {
                ChattingListTextBox.Text += "서버 연결에 성공하였습니다.\n";
            }
            else
            {
                ChattingListTextBox.Text += $"서버 연결에 실패했습니다. 사유: {packet.DeniedReason}\n";
            }
        }
    }
}
