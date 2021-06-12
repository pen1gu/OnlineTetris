using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Windows.Forms;

namespace TetrisMasterClient_jtg
{
    class User
    {
        public BoardBase boardBase;
        public String userName = "";
        public Panel board;

        public User(String userName, Panel board)
        {
            this.userName = userName;
            this.board = board;
        }

    }
}
