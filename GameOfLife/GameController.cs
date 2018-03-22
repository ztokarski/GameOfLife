using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameController
    {
        public Game Game { get; set; }
        public BoardController BoardController { get; set; }

        public GameController(int boardSize)
        {
            Game = new Game(new Board(boardSize));
            BoardController = new BoardController(Game.Board);
        }

        public void Run()
        {
            LoadExample(SmallExploder);

            var nextRound = true;
            int counter = 0;
            int score = 0;

            BoardController.DisplayBoard();

            string control = AutosimulationInput();

            if (AutosimulationOn(control))
            {
                while (nextRound)
                {

                    BoardController.DisplayBoard();

                    System.Threading.Thread.Sleep(1);

                    PrepareNextRound(ref nextRound, counter, ref score);
                }
            }

            else // use 'Enter' to simulate next round
            {
                while (nextRound)
                {
                    string answer = GetAnswerFromUser();
                    if (UserWantsExit(answer))
                    {
                        nextRound = false;
                    }
                    else
                    {
                        BoardController.DisplayBoard();

                        PrepareNextRound(ref nextRound, counter, ref score);

                        Console.WriteLine("Press 'e' to exit");

                    }
                }

            }

        }

        private static bool UserWantsExit(string answer)
        {
            return answer.ToUpper() == "E";
        }

        private static string GetAnswerFromUser()
        {
            Console.WriteLine("Press 'Enter' to continue");
            var answer = Console.ReadLine();
            return answer;
        }

        private static string AutosimulationInput()
        {
            Console.WriteLine("Autosimulation? Y/N");
            var control = Console.ReadLine();
            return control;
        }

        private void PrepareNextRound(ref bool nextRound, int counter, ref int score)
        {
            BoardController.PrepareCellsForNextTurn();
            nextRound = CheckIfBoardChanged(nextRound, counter);
            score = ScoreUpdateAndDisplay(score);
            BoardController.UpdateBoard();
        }

        private static int ScoreUpdateAndDisplay(int score)
        {
            score++;
            Console.WriteLine(String.Format("Your score: {0}", score));
            return score;
        }

        private static bool AutosimulationOn(string control)
        {
            return control.ToUpper() == "Y";
        }

        private bool CheckIfBoardChanged(bool nextRound, int counter)
        {
            if (IsBoardNotChanged(counter))
            {
                nextRound = false;
            }
            return nextRound;
        }

        private bool IsBoardNotChanged(int counter)
        {
            foreach (var cell in Game.Board.Cells)
            {
                if (cell.IsAlive == cell.IsAliveNextTurn)
                {
                    counter++;
                }
                if (counter == Game.Board.Cells.Count)
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadExample(List<int> coordinates)
        {
            for (int i = 0; i < coordinates.Count; i += 2)
            {
                BoardController.SetCellAlive(coordinates[i], coordinates[i + 1]);
            }
        }

        List<int> test2 = new List<int> { 20, 16, 20, 17, 20, 18, 21, 18, 21, 17, 21, 19, 22, 18 };
        List<int> ShapeM = new List<int> { 19, 16, 18, 16, 17, 16, 16, 16, 15, 16, 15, 17, 16, 18, 17, 19, 16, 20, 15, 21, 15, 22, 16, 22, 17, 22, 18, 22, 19, 22 };
        List<int> ShapeC = new List<int> { 20, 20, 20, 19, 21, 18, 22, 17, 23, 17, 24, 17, 25, 18, 26, 19, 26, 20 };
        List<int> Glider = new List<int> { 20, 15, 20, 16, 20, 17, 19, 17, 18, 16 };
        List<int> TenCellRow = new List<int> { 10, 0, 10, 1, 10, 2, 10, 3, 10, 4, 10, 5, 10, 6, 10, 7, 10, 8, 10, 9 };
        List<int> SmallExploder = new List<int> { 10, 15, 11, 14, 11, 15, 11, 16, 12, 14, 12, 16, 13, 15 };

    }
}
