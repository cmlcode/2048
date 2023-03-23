namespace _2048Tests
{
    using _2048;
    [TestClass]
    public class GameManagerTests
    {
        [TestMethod]
        public void Constructor_noTerminal()
        {
            GameManager ManagerObj;

            ManagerObj = new GameManager(false);

            Assert.AreEqual(0, ManagerObj.Score);
            Assert.AreEqual(0, ManagerObj.HighScore);
        }

        public void Constructor_noTerminal_setRand()
        {
            GameManager ManagerObj;
            Random rand;

            rand = new Random();
            ManagerObj = new GameManager(false, rand);

            Assert.AreEqual(0, ManagerObj.Score);
            Assert.AreEqual(0, ManagerObj.HighScore);
            Assert.AreEqual(rand, ManagerObj.BoardObj.rnd);
        }

        [TestMethod]
        public void PlayAgain_Yes_Test()
        {
            GameManager ManagerObj;
            bool PlayAgainVal;

            ManagerObj = new GameManager(false);
            PlayAgainVal = ManagerObj.PlayAgain(ConsoleKey.Y);

            Assert.IsTrue(PlayAgainVal);
        }

        [TestMethod]
        public void PlayAgain_No_Test()
        {
            GameManager ManagerObj;
            bool PlayAgainVal;

            ManagerObj = new GameManager(false);
            PlayAgainVal = ManagerObj.PlayAgain(ConsoleKey.N);

            Assert.IsFalse(PlayAgainVal);  
        }

        [TestMethod]
        public void ResetGame()
        {
            GameManager ManagerObj;
            Board OldBoard;

            ManagerObj = new GameManager(false);
            OldBoard = ManagerObj.BoardObj;
            ManagerObj.IncreaseScore(100);
            ManagerObj.ResetGame();

            Assert.AreEqual(ManagerObj.Score, 0);
            Assert.AreEqual(ManagerObj.HighScore, 100);
            Assert.AreNotEqual(OldBoard, ManagerObj.BoardObj);
        }

        [TestMethod]
        public void GetDirection_KeyUp()
        {
            GameManager ManagerObj;
            String NewDirection;

            ManagerObj = new GameManager(false);
            NewDirection = ManagerObj.GetDirection(ConsoleKey.UpArrow);

            Assert.AreEqual(NewDirection, "up");
        }

        [TestMethod]
        public void GetDirection_KeyRight()
        {
            GameManager ManagerObj;
            String NewDirection;

            ManagerObj = new GameManager(false);
            NewDirection = ManagerObj.GetDirection(ConsoleKey.RightArrow);

            Assert.AreEqual(NewDirection, "right");
        }

        [TestMethod]
        public void GetDirection_KeyLeft()
        {
            GameManager ManagerObj;
            String NewDirection;

            ManagerObj = new GameManager(false);
            NewDirection = ManagerObj.GetDirection(ConsoleKey.LeftArrow);

            Assert.AreEqual(NewDirection, "left");
        }

        [TestMethod]
        public void GetDirection_KeyDown()
        {
            GameManager ManagerObj;
            String NewDirection;

            ManagerObj = new GameManager(false);
            NewDirection = ManagerObj.GetDirection(ConsoleKey.DownArrow);

            Assert.AreEqual(NewDirection, "down");
        }

        [TestMethod]
        public void GetDirection_WrongInput()
        {
            GameManager ManagerObj;
            String NewDirection;

            ManagerObj = new GameManager(false);
            NewDirection = ManagerObj.GetDirection(ConsoleKey.A);

            Assert.AreEqual(NewDirection, "");
        }

        [TestMethod]
        public void IncreaseScore_PositiveValue()
        {
            GameManager ManagerObj;
            bool FunctionRuns;


            ManagerObj = new GameManager(false);
            FunctionRuns = ManagerObj.IncreaseScore(20);

            Assert.AreEqual(ManagerObj.Score, 20);
            Assert.IsTrue(FunctionRuns);
        }

        [TestMethod]
        public void IncreaseScore_NegativeValue()
        {
            GameManager ManagerObj;
            bool FunctionRuns;

            ManagerObj = new GameManager(false);
            FunctionRuns = ManagerObj.IncreaseScore(-5);

            Assert.AreEqual(ManagerObj.Score, 0);
            Assert.IsFalse(FunctionRuns);
        }

        [TestMethod]
        public void HasWinner_Empty()
        {
            GameManager ManagerObj;
            bool GameOver;

            ManagerObj = new GameManager(false);
            GameOver = ManagerObj.HasWinner();

            Assert.IsFalse(GameOver);
        }
    }
}