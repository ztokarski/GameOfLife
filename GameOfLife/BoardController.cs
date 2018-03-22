using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class BoardController
    {
        public Board Board { get; set; }

        public BoardController(Board board)
        {
            Board = board;
        }

        public Cell GetCell(int x, int y)
        {
            foreach (var cell in Board.Cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    return cell;
                }
            }
            return null;
        }

        public void SetCellAlive(int x, int y)
        {
            GetCell(x, y).IsAlive = true;
        }

        public void SetCellAlive(Cell cell)
        {
            cell.IsAlive = true;
        }

        public void SetCellAliveNextTurn(Cell cell)
        {
            cell.IsAliveNextTurn = true;
        }

        public void SetCellNotAliveNextTurn(Cell cell)
        {
            cell.IsAliveNextTurn = false;
        }

        public void DisplayBoard()
        {
            Console.Clear();
            int counter = 0;
            while (counter < Math.Sqrt(Board.Cells.Count))
            {
                for (int i = 0; i < Board.Cells.Count; i++)
                {
                    if (Board.Cells[i].X == counter)
                    {
                        Console.Write(Board.Cells[i]);
                    }
                }
                Console.WriteLine("");
                counter++;
            }
        }

        public void PrepareCellsForNextTurn()
        {
            foreach (var cell in Board.Cells)
            {
                if (CellIsNotAlive(cell))
                {
                    if (CellHas3neighbours(cell))
                    {
                        SetCellAliveNextTurn(cell);
                    }
                }
                else // cell is Alive
                {
                    if (CellHas2or3neighbours(cell))
                    {
                        SetCellAliveNextTurn(cell);
                    }
                    else // cell has NOT 2 or 3 neighbours
                    {
                        SetCellNotAliveNextTurn(cell);
                    }
                }
            }
        }

        private bool CellHas2or3neighbours(Cell cell)
        {
            return GetActiveNeighbours(cell) == 2 || GetActiveNeighbours(cell) == 3;
        }

        private bool CellHas3neighbours(Cell cell)
        {
            return GetActiveNeighbours(cell) == 3;
        }

        private static bool CellIsNotAlive(Cell cell)
        {
            return cell.IsAlive == false;
        }

        public void UpdateBoard()
        {
            foreach (var cell in Board.Cells)
            {
                cell.IsAlive = cell.IsAliveNextTurn;
            }
        }

        public int GetActiveNeighbours(Cell cell)
        {
            int activeNeighboursNum = 0;

            if (UpperLeftCorner(cell))
            {
                for (int i = cell.X; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (DownLeftCorner(cell))
            {
                for (int i = cell.X - 1; i < cell.X + 1; i++)
                {
                    for (int j = cell.Y; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (UpRightCorner(cell))
            {
                for (int i = cell.X; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 1; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (DownRightCorner(cell))
            {
                for (int i = cell.X - 1; i < cell.X + 1; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 1; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (UpperEdge(cell))
            {
                for (int i = cell.X; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (BottomEdge(cell))
            {
                for (int i = cell.X - 1; i < cell.X + 1; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (LeftEdge(cell))
            {
                for (int i = cell.X - 1; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }
            else if (RightEdge(cell))
            {
                for (int i = cell.X - 1; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 1; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }

            else // cells in the middle of board
            {
                for (int i = cell.X - 1; i < cell.X + 2; i++)
                {
                    for (int j = cell.Y - 1; j < cell.Y + 2; j++)
                    {
                        activeNeighboursNum = UpdateCounter(cell, activeNeighboursNum, i, j);
                    }
                }
            }

            return activeNeighboursNum;
        }

        private int UpdateCounter(Cell cell, int counter, int i, int j)
        {
            if (NeighbourIsAlive(cell, i, j))
            {
                counter++;
            }
            return counter;
        }

        private bool NeighbourIsAlive(Cell cell, int i, int j)
        {
            return GetCell(i, j).IsAlive && GetCell(i, j) != cell;
        }

        private bool BottomEdge(Cell cell)
        {
            return cell.X == Board.SizeX;
        }

        private static bool LeftEdge(Cell cell)
        {
            return cell.Y == 0;
        }

        private bool RightEdge(Cell cell)
        {
            return cell.Y == Board.SizeY;
        }

        private static bool UpperEdge(Cell cell)
        {
            return cell.X == 0;
        }


        private bool UpRightCorner(Cell cell)
        {
            return cell.X == 0 && cell.Y == Board.SizeY;
        }

        private bool DownRightCorner(Cell cell)
        {
            return cell.X == Board.SizeX && cell.Y == Board.SizeY;
        }

        private bool DownLeftCorner(Cell cell)
        {
            return cell.X == Board.SizeX && cell.Y == 0;
        }

        private static bool UpperLeftCorner(Cell cell)
        {
            return cell.X == 0 && cell.Y == 0;
        }

    }
}
