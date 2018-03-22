using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Board
    {
        private const double aspectRatio = 1.5;

        public List<Cell> Cells { get; set; }
        public int SizeX { get; set; }
        public double SizeY { get; set; }

        public Board(int boardSize)
        {
            Cells = CreateCells(boardSize);
            SizeX = boardSize - 1;
            SizeY = (boardSize * aspectRatio) - 1;
        }

        private List<Cell> CreateCells(int boardSize)
        {
            var cells = new List<Cell>();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize * aspectRatio; j++)
                {
                    cells.Add(new Cell(new int[] { i, j }));
                }
            }
            return cells;
        }
    }
}
