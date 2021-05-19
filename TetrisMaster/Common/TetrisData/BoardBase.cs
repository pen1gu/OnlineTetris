using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    
    public class BoardBase
    {
        public PieceType[,] Board { get; set; }
        public readonly int ColumnCnt = 20;
        public readonly int RowCnt = 10;

        public BoardBase()
        {
            Board = new PieceType[ColumnCnt, RowCnt];
        }

    }
}
