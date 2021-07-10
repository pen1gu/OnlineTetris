using Common;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisMasterClient_yhj
{
    public class TetrisInfo
    {
        SocketEx connection;
        Board board;

        public TetrisInfo(Board board)
        {
            this.board = board;
        }



        public async void sendBoard()
        {
            await connection.SendMessageAsync(new CS_Board()
            {
                Board = board 
            });
        }
    }
}