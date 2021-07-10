using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common
{
    public abstract class BoardBase 
    {
        public Cell[,] Board { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public abstract void MoveRight();
        public abstract void MoveLeft();
        public abstract void MoveDown();
        public abstract void MoveUp();
    }

    public class Cell
    {
        public CellType CellType { get; set; }
         public CellColor Color { get; set; }
    }

    public enum CellType
    {
        Empty,
        Fill,
        Active,
    }

    public enum CellColor
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
    }
}
