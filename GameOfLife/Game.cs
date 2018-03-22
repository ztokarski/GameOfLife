using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Game
    {
        public Board Board { get; set; }
        public Player Player { get; set; }
        public int Result { get; set; }

        public Game(Board board)
        {
            Board = board;
            Player = new Player("Jan");
            Result = 0;
        }

       

        
    }
}
