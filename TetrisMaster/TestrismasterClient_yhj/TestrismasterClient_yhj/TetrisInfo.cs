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
        public Boolean isEnded = false;
        public Form1 form1;
        User user;
        SocketEx connection;
        BoardBase boardBase;
        const int Width = 10, Height = 20;
        int CurrentX = 4, CurrentY = 0;
        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        int[,] Board = new int[Width, Height];
        int[,] OldBoard = new int[Width, Height];

        public TetrisInfo()
        {

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Board[i, j] = 0;
                    OldBoard[i, j] = 1;
                }
            }

        }


        public async void Run(Form1 form1, User user, SocketEx connection)
        {
            this.user = user;
            this.connection = connection;
            this.form1 = form1;


            boardBase = new BoardBase();
            //BoardBase file 참고하여 Cell type 지정

             await MoveBlockDownLooplyAsync();
        }

        public void Rotate() 
        {
            // F 키 누를 시 회전
            sendBoard();
        }

        public void CheckLine()
        {
            // 상시 대기하면서 한 줄이 다 찰시 조건 만족
            sendBoard();
        }

        public void MoveRight()
        {
            RemoveCurrentBlock();

            CurrentX++;
            MergeCurrentBlockToBoard();
            sendBoard();
        }
        public void MoveLeft()
        {
            RemoveCurrentBlock();

            CurrentX--;
            MergeCurrentBlockToBoard();
            sendBoard();
        }
        public void MoveDown()
        {
            RemoveCurrentBlock();
                CurrentY++;
            try
            {

                MergeCurrentBlockToBoard();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            sendBoard();
        }

        private void RemoveCurrentBlock() // 이전에 위치했던 블록 위치 삭제
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Board[i, j] == 2)
                    {
                        Board[i, j] = 0;
                    }
                }
            }
        }


        public void DrawBoard(Form form) // 화면에 그리기 unitsize -> 한 칸 크기 (30,30)
        {
            int unitSize = 20;

            int marginLeft = 1 * unitSize;
            int marginTop = -1 * unitSize;

            float width = 10 * unitSize;
            float height = 20 * unitSize;

            float x1 = marginLeft;
            float y1 = marginTop;
            float x2 = x1 + width;
            float y2 = y1 + height;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 4; j < Height; j++)
                {
                    x1 = marginLeft + (i * unitSize);
                    y1 = marginTop + (j * unitSize);

                    try
                    {
                        if (Board[i, j] != OldBoard[i, j])
                        {
                            PaintCell(form, unitSize, x1, y1, i, j);
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    OldBoard[i, j] = Board[i, j];
                }
            }
        }

        private void PaintCell(Form form, int unitSize, float x1, float y1, int i, int j) // 그릴 폼, 한 블록의 크기, x, y 좌표, i,j 번째의 방향
        {
            Graphics g;

            try
            {
                g = form.CreateGraphics();
            }
            catch
            {
                return;
            }
            switch (Board[i, j])
            {
                case 1: 
                    g.FillRectangle(Brushes.White, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.Black), x1, y1, unitSize, unitSize);
                    break;
                case 2:
                    g.FillRectangle(Brushes.Red, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.Black), x1, y1, unitSize, unitSize);
                    break;
                default:
                    g.FillRectangle(Brushes.Black, x1, y1, unitSize, unitSize);
                    break;
            }
        }

        private void MergeCurrentBlockToBoard()
        {
            try { 
                Board[CurrentX, CurrentY] = 2;
            }
            catch
            {
                return;
                // TODO:
            }
            form1.setLog("X: " + CurrentX + ", Y: " + CurrentY+"\n");
            // 여기서 Board를 나가는 에러가 발생
        }

      
        public void NewBlock()
        {
            CurrentX = 4;
            CurrentY = 0;

            /*SetRandomCurrentBlock();*/
            MergeCurrentBlockToBoard();
        }


        //TODO: 최대 보드 길이 설정

        private async void sendBoard()
        { 
            await connection.SendMessageAsync(new CS_Board()
            {
                Board = boardBase
            });
        }

        public async Task<bool> MoveBlockDownLooplyAsync() // 동작 부분
        {
            while (true) // 계속해서 돌면서 delay 150을 넣음
            {
                await semaphoreSlim.WaitAsync();
                MoveDown();

                    DrawBoard(form1);
                    semaphoreSlim.Release();

                    /*if (IsGameOver())
                    {
                        break;
                    }*/

                    /*Text = CurrentBlock.ToString();*/
                    sendBoard();
                    await Task.Delay(150);
            }

            return true;
        }

    }
}