using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisMasterClient_yhj
{

    public class User
    {
        Board board { get; set; }
        string userName { get; set; }

        public User(string userName)
        {
            this.userName = userName; // 유저 
            this.board = new Board(5,10);
        }

        public Board getBoard()
        {
            return board;
        }
    }
}
