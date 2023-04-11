using System;
using System.ComponentModel;
using System.Reactive.Subjects;

namespace _2048
{
    public class GameManager
    {
        //Player's current score
        private int _score;
        public int Score
        {
            get => _score;
            private set => _score = value;
        }

        //Player's highscore
        private int _highScore = 0;
        public int HighScore
        {
            get => _highScore;
            private set
            {
                if (value > _highScore)
                {
                    _highScore = value;
                }
            }
        }

        //Board being played on
        public Board BoardObj;

        public GameManager(bool runTerminal)
        {
            /*
             * Creates new game 
             * If game runs in terminal, starts the game and handles new game
             */
            Score = 0;

            //Creates new board to play on
            BoardObj = new Board(this);

            //If the game is played on the terminal, handle game start and end
            if (runTerminal){
                GameLoop();
                if (GetPlayAgainInput())
                {
                    ResetGame();
                    GameLoop();
                    return;
                }
            }
        }

        public GameManager(bool runTerminal, Random rand)
        {
            /*
             * Game manager with set random value for board creation
             * Follows logic of previous game manager
             */
            Score = 0;
            HighScore = Score;
            BoardObj = new Board(this, rand);

            if (runTerminal)
            {
                GameLoop();
                if (GetPlayAgainInput())
                {
                    ResetGame();
                    GameLoop();
                    return;
                }
            }
        }

        public bool GetPlayAgainInput()
        {
            // Gets user input on whether to play again or not

            //Get user input
            Console.WriteLine("Play again? (Y/N)");
            ConsoleKey InputKey = Console.ReadKey().Key;

            //interpret user input, guarentees that input is a Y or N
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

        public bool GetPlayAgainInput(ConsoleKey UserInput)
        {
            //Used to test GetPlayAgainInput() with set inputs
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
            //Resets the game state
            Score = 0;
            BoardObj = new Board(this);
        }
        

        public void GameLoop()
        {
            //Handles game turns

            while (true)
            {
                BoardObj.AddTile();
                PrintBoard();
                if (CheckIfGameOver()) {
                    Console.WriteLine("GAME OVER");
                    return;
                }
                MoveTiles();
            }
        }

        public string GetDirection(ConsoleKey InputKey)
        {
            /*
             * Converts user input to string determining direction
             * Used to filter out wrong inputs 
             */
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
                    return "";
            }
        }

        public void MoveTiles()
        {
            //Determines whether player's move is possible and makes the move
            bool hasMoved = false;
            while(!hasMoved)
            {
                //Checks if user's input is a possible option
                string direction = "";
                while (direction.Equals(""))
                {
                    direction = GetDirection(Console.ReadKey().Key);
                }

                //Checks if the user's input is currently possible
                hasMoved = BoardObj.MoveTiles(direction);
            }   
        }

        public bool MoveTiles(ConsoleKey UserInput)
        {
            //Used to test GetPlayAgainInput() with set inputs
            string direction = GetDirection(UserInput);

            if(direction == "")
            {
                return false;
            }

            return BoardObj.MoveTiles(direction);
        }

        public void PrintBoard()
        {
            //Prints the active state of the board
            Console.Clear();
            Console.WriteLine("Score: {0}\tHighScore: {1}", Score,HighScore);
            Console.WriteLine(BoardObj.GetBoard());
        }

        public bool IncreaseScore(int val)
        {
            /*
             * Increases score by set amount, checks that input is a positive value
             * Increases highscore if necessary
             */
            if (val < 0)
            {
                return false; 
            }
            Score += val;
            if (Score > HighScore) HighScore = Score;
            return true;
        }

        public bool CheckIfGameOver()
        {
            //Determines whether the game is finished, returns true if so

            //Game is not over if there's still open spots
            if (!BoardObj.BoardFull())
            {
                return false;
            }
            //Checks if tile is equal to tile to the right and below it
            for (int TileLoc = 0; TileLoc < 16; TileLoc++)
            {
                //Tile has to have a value due to BoardFull check
                int TileVal = (int)BoardObj.GetTile(TileLoc).TileVal;

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

            //If doesn't meet any of the above conditions, the game is over and return true
            return true;
        }

    }
}
