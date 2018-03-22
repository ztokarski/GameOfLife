using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Player
    {
        public string Name { get; set; }
       

        public Player(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
