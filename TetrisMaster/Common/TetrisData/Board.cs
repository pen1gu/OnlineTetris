using System;
using System.Collections.Generic;
using System.Text;

namespace Common.TetrisData
{
    
    public class Board
    {
        public PieceType[,] board { get; set; }
        public readonly int ColumnCnt = 20;
        public readonly int RowCnt = 10;

        public Board()
        {
            board = new PieceType[ColumnCnt, RowCnt];
        }

    }
}
