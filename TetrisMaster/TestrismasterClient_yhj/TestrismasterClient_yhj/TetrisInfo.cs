using Common;
using System;
using System.Threading.Tasks;

namespace TetrisMasterClient_yhj
{
    class TetrisInfo
    {
        public Boolean isEnded = false;
        User user;
        SocketEx connection;
        BoardBase boardBase;

        int w_index = 4, h_index = 0;

        public TetrisInfo(SocketEx connection, User user)
        {
            this.connection = connection;
            this.user = user;
            
        }

        public async Task Run() {

            boardBase = user.getBoardBase();
            for(int i = 0; i < 20; i++)
            {

            }

            while (!isEnded)
            {

                await Task.Delay(1000);
                
    
                await StepDownBlock();
                sendBoard();
            }
        }

        private async Task Rotate()
        {
            // F 키 누를 시 회전
            sendBoard();
        }

        private async Task CheckLine()
        {
            // 상시 대기하면서 한 줄이 다 찰시 조건 만족
            sendBoard();
        }

        public void MoveRight()
        {
            boardBase.Board[h_index, w_index + 1] = PieceType.A;
            sendBoard();
        }
        public void MoveLeft()
        {
            boardBase.Board[h_index, w_index - 1] = PieceType.A;
            sendBoard();
        }
        public void MoveDown()
        {
            boardBase.Board[h_index + 1, w_index] = PieceType.A;
            sendBoard();
        }

        private void DropLastBlock()
        {
            boardBase.Board[h_index, w_index] = 0;
        }

        public async Task MoveEnd()
        {
            await StepDownBlock();
            sendBoard();
        }

        private async Task StepDownBlock()
        {
            // 기본적으로 내려오는 것

            for(int i = 0; i < boardBase.RowCnt; i++)
            {
                for(int j = 0; j < boardBase.ColumnCnt; j++)
                {
                    /*if(boardBase.Board[i+1,j+1] != -1){
                         boardBase.Board[i+1][j+1] += 1
                     }*/
                }
            }

            sendBoard();
        }

        private async void sendBoard()
        {
            DropLastBlock();

            await connection.SendMessageAsync(new CS_Board()
            {
                Board = boardBase
            });
        }
    }
}
