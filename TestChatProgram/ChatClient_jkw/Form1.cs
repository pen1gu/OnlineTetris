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
    public partial class Form1 : Form
    {
        Socket connection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private async void ConnectServerButton_Click(object sender, EventArgs e)
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
            IPAddress ipAddress = IPAddress.Parse("221.143.21.37");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 52217);

            // Create a TCP/IP socket.  
            try
            {
                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                await client.ConnectAsync(remoteEP);

                richTextBox1.Text += $"Connected: {client.Connected} \n";

                connection = client;

                await HandleReceiveAsync();
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "Error: " + ex.Message;
                connection?.Close();
                connection = null;
            }
       }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var connected = connection?.Connected ?? false;
                if (!connected)
                {
                    MessageBox.Show("연결 먼저 하세요");
                    return;
                }

                Task.Run(async () =>
                {
                    var buffer = Encoding.UTF8.GetBytes(MessageTextBox.Text);
                    await connection.SendAsync(buffer, SocketFlags.None);
                }).Wait();
            }
        }

        private async Task HandleReceiveAsync()
        {
            var buffer = new byte[10000];
            while (true)
            {
                var receiveCount = await connection.ReceiveAsync(buffer, SocketFlags.None);
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
                var receiveData = Encoding.UTF8.GetString(buffer, 0, receiveCount);
                richTextBox1.Text += receiveData + "\n";
            }
        }
    }
}
