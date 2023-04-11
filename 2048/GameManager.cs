using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace _2048
{
    public class GameManager : INotifyPropertyChanged
    {
        private readonly BehaviorSubject<int> _score = new BehaviorSubject<int>(0);
        internal void NotifyPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public int Score
        {
            get => _score.Value;
            private set => _score.OnNext(value);
        }
        private int _highScore;
        public int HighScore
        {
            get => _highScore;
            private set => _highScore = value;
        }
        public Board BoardObj;
        public event PropertyChangedEventHandler PropertyChanged;
        public IObservable<int> ScoreObservable => _score;

        public GameManager(bool runTerminal)
        {
            Score = 0;
            HighScore = Score;
            BoardObj = new Board(this);
            if (runTerminal){
                GameLoop();
                if (PlayAgain())
                {
                    ResetGame();
                    GameLoop();
                    return;
                }
            }
        }

        public GameManager(bool runTerminal, Random rand)
        {
            Score = 0;
            HighScore = Score;
            BoardObj = new Board(this, rand);
            if (runTerminal)
            {
                GameLoop();
                if (PlayAgain())
                {
                    ResetGame();
                    GameLoop();
                    return;
                }
            }
        }

        public bool PlayAgain()
        {
            Console.WriteLine("Play again?(Y/N)");
            ConsoleKey InputKey = Console.ReadKey().Key;
            while (InputKey != ConsoleKey.Y || InputKey != ConsoleKey.N)
            {
                if (InputKey == ConsoleKey.Y)
                {
                    return true;
                }
                if (InputKey == ConsoleKey.N)
                {
                    return false;
                }
                InputKey = Console.ReadKey().Key;
            }
            return false;
        }

        public bool PlayAgain(ConsoleKey UserInput)
        {
            Console.WriteLine("Play again?(Y/N)");
            ConsoleKey InputKey = UserInput;
            while (InputKey != ConsoleKey.Y || InputKey != ConsoleKey.N)
            {
                if (InputKey == ConsoleKey.Y)
                {
                    return true;
                }
                if (InputKey == ConsoleKey.N)
                {
                    return false;
                }
                InputKey = Console.ReadKey().Key;
            }
            return false;
        }

        public void ResetGame()
        {
            Score = 0;
            BoardObj = new Board(this);
        }
        

        public void GameLoop()
        {
            while (true)
            {
                BoardObj.AddTile();
                PrintScreen();
                if (HasWinner()) {
                    Console.WriteLine("GAME OVER");
                    return;
                }
                MoveTiles();
            }
        }

        public string GetDirection(ConsoleKey InputKey)
        {
            switch (InputKey)
            {
                case ConsoleKey.UpArrow:
                    return "up";
                case ConsoleKey.RightArrow:
                    return "right";
                case ConsoleKey.LeftArrow:
                    return "left";
                case ConsoleKey.DownArrow:
                    return "down";
                default:
                    return"";
            }
        }

        public void MoveTiles()
        {
            bool hasMoved = false;
            while(!hasMoved)
            {
                string direction = "";
                while (direction.Equals(""))
                {
                    direction = GetDirection(Console.ReadKey().Key);
                }
                hasMoved = BoardObj.MoveTiles(direction);
            }   
        }

        public bool MoveTiles(ConsoleKey UserInput)
        {
            string direction = GetDirection(UserInput);

            if(direction == "")
            {
                return false;
            }

            return BoardObj.MoveTiles(direction);
        }

        public void PrintScreen()
        {
            Console.Clear();
            Console.WriteLine("Score: {0}\tHighScore: {1}", Score,HighScore);
            Console.WriteLine(BoardObj.GetBoard());
        }

        public bool IncreaseScore(int val)
        {
            if (val < 0)
            {
                return false; 
            }
            Score += val;
            if (Score > HighScore) HighScore = Score;
            return true;
        }

        public bool HasWinner()
        {
            if (!BoardObj.BoardFull())
            {
                return false;
            }
            //Repeats checking some tiles, come up with more optimized solution
            for (int TileLoc = 0; TileLoc < 16; TileLoc++)
            {
                int? TileVal = BoardObj.GetTile(TileLoc).TileVal;
                if(TileVal == null)
                {
                    return false;
                }
                //Checks if tile is equal to the tile below it, doesn't run if tile is bottom tile
                if (TileLoc < 12 && TileVal == BoardObj.GetTile(TileLoc + 4).TileVal)
                {
                    return false;
                }
                //Check if the tile is equal to the tile on its right, doesn't run if tile is a rightmost tile
                if (TileLoc % 4 != 3 && TileVal == BoardObj.GetTile(TileLoc + 1).TileVal)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
