using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int[] Position { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAliveNextTurn { get; set; }
   

        public Cell(int[] position)
        {
            Position = position;
            IsAlive = false;
            IsAliveNextTurn = false;
            X = position[0];
            Y = position[1];
        }

        public override string ToString()
        {
            if (IsAlive == true)
            {
                return "█";
            }
            return " ";
        }

    }
}
