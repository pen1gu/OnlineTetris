using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisMasterClient_yhj
{

    public class User
    {
        BoardBase board { get; set; }
        string userName { get; set; }

        //bool endGame = false;

        public User(string userName)
        {
            this.userName = userName; // 유저 로그인
            board = new BoardBase(); // 사용자 보드 생성
        }

        public BoardBase getBoardBase()
        {
            return board;
        }

    }
}
