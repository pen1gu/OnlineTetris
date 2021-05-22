using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TetrisMasterClient_jtg
{
    class TetrisThread
    {

        public Boolean isEnded = false;

        public void Run()
        {
            var image = Image.FromFile("images\tiles.png");
            
            while (!isEnded)
            {

            }
        }

        public void Rotate()
        {

        }

        public void CheckLine()
        {

        }
    }
}
