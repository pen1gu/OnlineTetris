using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TetrisMasterClient_yhj
{
    public class Board : BoardBase
    {
        TetrisInfo info; // 인스턴스 x
        int CurrentX;
        int CurrentY = 0;

        /*int[,] board;*/
        int[,] oldBoard;

        public Board(int width, int height)
        {
            Width = width > 20 ? 20 : width;
            Height = height;
            oldBoard = new int[Width, Height];
            Board = new Cell[Width, Height];

            CurrentX = Width / 2;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Board[i, j] = new Cell();
                    oldBoard[i, j] = 1; // Cell 변경
                }
            }
        }

        public override void MoveRight()
        {
            RemoveCurrentBlock();

            CurrentX++;
            MergeCurrentBlockToBoard();
        }
        public override void MoveLeft()
        {
            RemoveCurrentBlock();

            CurrentX--;
            MergeCurrentBlockToBoard();
        }

        public override void MoveUp()
        {
            throw new NotImplementedException();
        }

        public override void MoveDown()
        {
            RemoveCurrentBlock();
            CurrentY++;
            try
            {

                MergeCurrentBlockToBoard();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void RemoveCurrentBlock() // 이전에 위치했던 블록 위치 삭제
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if(Board[i,j].CellType == CellType.Fill)
                    {
                        Board[i, j].CellType = CellType.Empty;
                    }
                }
            }
        }

        private void MergeCurrentBlockToBoard()
        {
            try
            {
                Board[CurrentX, CurrentY].CellType = CellType.Fill;
            }
            catch
            {
                return;
                // TODO:
            }
            /*form1.setLog("X: " + CurrentX + ", Y: " + CurrentY + "\n");*/
            // 여기서 Board를 나가는 에러가 발생
        }

        public void DrawBoard(Form1 form) // 화면에 그리기 unitsize -> 한 칸 크기 (30,30)
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
                        /*if (Board[i, j] != oldBoard[i, j]) // Old 도 변경
                        {
                            PaintCell(form, unitSize, x1, y1, i, j);
                        }*/
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    /*oldBoard[i, j] = Board[i, j];*/
                }
            }
        }// Board 말고 form으로 변경


        public void NewBlock()
        {
            CurrentX = Width / 2;
            CurrentY = 0;

            /*SetRandomCurrentBlock();*/
            MergeCurrentBlockToBoard();
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
            switch (Board[i, j].CellType)
            {
                case CellType.Active:
                    g.FillRectangle(Brushes.White, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.Black), x1, y1, unitSize, unitSize);
                    break;
                case CellType.Fill:
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
