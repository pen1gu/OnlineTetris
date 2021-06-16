using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp3
{
    public class Tetris
    {
        public const int Width = 10;
        public const int Height = 24;
        public const int HiddenBoardHeight = 4;

        public int[,] Board;
        public int[,] OldBoard;

        public int CurrentBlock = 0;
        public int CurrentDirection = 0;
        public int CurrentX = 4;
        public int CurrentY = 0;

        private Random random = new Random((int)DateTime.Now.Ticks);

        public Tetris()
        {
            Board = new int[Width, Height];
            OldBoard = new int[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Board[i, j] = 0;
                    OldBoard[i, j] = 1;
                }
            }
        }

        public void NewBlock()
        {
            CurrentX = 4; 
            CurrentY = 0;

            SetRandomCurrentBlock();
            MergeCurrentBlockToBoard();
        }

        public void SetRandomCurrentBlock()
        {
            CurrentBlock = (int)random.Next(0, 7);
        }

        public void NextDirection()
        {
            int nextDirection = CurrentDirection;

            switch (CurrentDirection)
            {
                case 0:
                    nextDirection = 1;
                    break;
                case 1:
                    nextDirection = 2;
                    break;
                case 2:
                    nextDirection = 3;
                    break;
                case 3:
                    nextDirection = 0;
                    break;
            }

            RemoveCurrentBlock();

            if (CanAction(nextDirection, CurrentX, CurrentY))
            {
                CurrentDirection = nextDirection;
            }

            MergeCurrentBlockToBoard();
        }

        private void RemoveCurrentBlock()
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

        private void MergeCurrentBlockToBoard()
        {
            int[,] bloackArray = GetBlockArray(CurrentBlock, CurrentDirection);

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
                        Board[CurrentX + i, CurrentY + j] = 2;
                    }
                }
            }
        }

        private int[,] GetBlockArray(int block, int direction)
        {
            switch (block)
            {
                case 0:
                    // ##
                    // ##
                    switch (direction)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return new int[2, 2] { { 1, 1 }, { 1, 1 } };
                    }
                    break;
                case 1:
                    // ####
                    switch (direction)
                    {
                        case 0:
                        case 2:
                            return new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
                        case 1:
                        case 3:
                            return new int[4, 4] { { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 } };
                    }
                    break;
                case 2:
                    //  ##
                    // ##
                    switch (direction)
                    {
                        case 0:
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
                        case 1:
                        case 3:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 0, 1 } };
                    }
                    break;
                case 3:
                    // ##
                    //  ##
                    switch (direction)
                    {
                        case 0:
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 0 }, { 0, 1, 1 } };
                        case 1:
                        case 3:
                            return new int[3, 3] { { 0, 0, 1 }, { 0, 1, 1 }, { 0, 1, 0 } };
                    }
                    break;
                case 4:
                    // ###
                    // #
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 3:
                            return new int[3, 3] { { 1, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
                    }
                    break;
                case 5:
                    // ###
                    //   #
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 1 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 } };
                        case 2:
                            return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 3:
                            return new int[3, 3] { { 0, 0, 1 }, { 0, 0, 1 }, { 0, 1, 1 } };
                    }
                    break;
                case 6:
                    // ###
                    //  #
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 0 } };
                        case 2:
                            return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 3:
                            return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    }
                    break;
            }

            return null;
        }

        public void MoveDown() // 아래로 떨어지는 움직임 ( 초당 1번 )
        {
            RemoveCurrentBlock();

            if (CanAction(CurrentDirection, CurrentX, CurrentY + 1))
            {
                CurrentY++;
                MergeCurrentBlockToBoard();
            }
            else
            {
                MergeCurrentBlockToBoard();
                FixBlock();
                NewBlock();
            }
        }

        private void FixBlock() // 한 라인이 채워졌음을 감지
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Board[i, j] == 2)
                    {
                        Board[i, j] = 1;
                    }
                }
            }
            DestroyBlock();
        }

        private void DestroyBlock() // 한 라인이 채워졌을 때 블록이 사라지는 동작
        {
            for (int j = Height - 1; j >= 0; j--)
            {
                bool isEveryCellfilled = true;

                for (int i = 0; i < Width; i++)
                {
                    if (Board[i, j] != 1)
                    {
                        isEveryCellfilled = false;
                    }
                }

                if (isEveryCellfilled)
                {
                    for (int k = j; k > 0; k--)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            Board[i, k] = Board[i, k - 1];
                        }
                    }
                }
            }
        }

        public bool IsGameOver() // 게임이 끝났음을 감지
        {
            for (int j = 0; j < Height; j++)
            {
                bool isEveryCellBlank = true;

                for (int i = 0; i < Width; i++)
                {
                    if (Board[i, j] != 0)
                    {
                        isEveryCellBlank = false;
                    }
                }
                if (isEveryCellBlank)
                {
                    return false;
                }
            }
            return true;
        }

        public void MoveLeft() // 왼쪽 움직임
        {
            RemoveCurrentBlock();

            if (CanAction(CurrentDirection, CurrentX - 1, CurrentY))
            {
                CurrentX--;
                MergeCurrentBlockToBoard();
            }
        }

        public void MoveRight() // 오른쪽 움직임
        {
            RemoveCurrentBlock();

            if (CanAction(CurrentDirection, CurrentX + 1, CurrentY))
            {
                CurrentX++;
                MergeCurrentBlockToBoard();
            }
        }

        private bool CanAction(int nextDirection, int nextX, int nextY) // 동작을 받았을 시 방향 설정 밑 다음 위치 설정
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
        }

        public void DrawBoard(Form form)
        {
            int unitSize = 30;

            int marginLeft = 1 * unitSize;
            int marginTop = -1 * unitSize;

            float width = Width * unitSize;
            float height = Height * unitSize;

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
    }
}
