using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace TetrisMasterClient_jtg
{
    class User
    {
        public BoardBase boardBase;
        public String userName = "";

        public User(String userName)
        {
            this.userName = userName;
        }

    }
}
