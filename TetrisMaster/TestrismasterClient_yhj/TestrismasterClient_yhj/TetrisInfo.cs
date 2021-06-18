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

        Form TaskForm;

        public async Task Run(Form TaskForm, User user, SocketEx connection)
        {
            this.user = user;
            this.connection = connection;
            this.TaskForm = TaskForm;

            boardBase = user.getBoardBase();

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
            MergeCurrentBlockToBoard();
            sendBoard();
        }

        /* public void DropLastBlock()
         {
             boardBase.Board[h_index, w_index] = 0;
         }
 */
        /*public async void MoveEnd()
        {
            await StepDownBlock();
            sendBoard();
        }*/

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
            int unitSize = 30;

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

                    if (Board[i, j] != OldBoard[i, j])
                    {
                        PaintCell(form, unitSize, x1, y1, i, j);
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
            Board[CurrentX, CurrentY] = 2;
        }

        /* private bool CanAction(int nextDirection, int nextX, int nextY) // 동작을 받았을 시 방향 설정 밑 다음 위치 설정
         {
             int[,] bloackArray = GetBlockArray(CurrentBlock, nextDirection);
             int arrayLength = bloackArray.Length;
             int size = 0;

             switch (arrayLength)
             {
                 case 4:
                     size = 2;
                     break;
                 case 9:
                     size = 3;
                     break;
                 case 16:
                     size = 4;
                     break;

             }



             for (int i = 0; i < size; i++)
             {
                 for (int j = 0; j < size; j++)
                 {
                     if (bloackArray[i, j] == 1)
                     {
                         if (nextY + j >= Height)
                         {
                             return false;
                         }

                         if (nextX + i < 0)
                         {
                             return false;
                         }

                         if (nextX + i >= Width)
                         {
                             return false;
                         }

                         if (Board[nextX + i, nextY + j] != 0)
                         {
                             return false;
                         }
                     }
                 }
             }
             return true;
         }*/

        public void NewBlock()
        {
            CurrentX = 4;
            CurrentY = 0;

            /*SetRandomCurrentBlock();*/
            MergeCurrentBlockToBoard();
        }

        /*public void StepDownBlock()
        {
            // 기본적으로 내려오는 것


            for (int i = 0; i < boardBase.RowCnt; i++)
            {
                for (int j = 0; j < boardBase.ColumnCnt; j++)
                {
                    if (boardBase.Board[i + 1, j + 1] != 0)
                    {
                        boardBase.Board[i + 1, j + 1] += 1;
                    }
                }
            }

            sendBoard();
        }*/

        private async void sendBoard()
        { 
            await connection.SendMessageAsync(new CS_Board()
            {
                Board = boardBase
            });
        }

        public async Task<bool> MoveBlockDownLooplyAsync()
        {
            while (true) // 계속해서 돌면서 delay 150을 넣음
            {
                await semaphoreSlim.WaitAsync();
                MoveDown();
                semaphoreSlim.Release();
                DrawBoard(TaskForm);

                /*if (Tetris.IsGameOver())
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