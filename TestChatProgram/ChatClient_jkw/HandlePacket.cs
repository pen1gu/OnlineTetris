using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient_jkw
{
    public partial class Form1
    {
        private void Handle_SC_LoginAllow(SC_LoginAllow packet)
        {
            if (packet.Allow)
            {
                richTextBox1.Text += "서버 연결에 성공하였습니다.\n";
            }
            else
            {
                richTextBox1.Text += $"서버 연결에 실패했습니다. 사유: {packet.DeniedReason}\n";
            }
        }
    }
}
