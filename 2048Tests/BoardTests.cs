using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Tests
{
    using _2048;
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Constructor_NoRandInput()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;

            Assert.AreEqual(BoardObj.EmptyTiles.Count,16);
            Assert.IsNull(BoardObj.GetTile(0));
        }

        [TestMethod]
        public void Constructor_RandInput()
        {
            GameManager ManagerObj;
            Board BoardObj;
            Random rand;

            rand = new Random();
            ManagerObj = new GameManager(false, rand);
            BoardObj = ManagerObj.BoardObj;

            Assert.AreEqual(BoardObj.EmptyTiles.Count, 16);
            Assert.IsNull(BoardObj.GetTile(0));
            Assert.AreEqual(BoardObj.rnd, rand);
        }

        [TestMethod]
        public void AddTile_SetLoc_PossibleVal()
        {
            int TileLoc;
            GameManager ManagerObj;
            Board BoardObj;
            bool TileAdded;

            TileLoc = 0;
            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            TileAdded = BoardObj.AddTile(TileLoc);

            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(TileLoc));
            Assert.IsNotNull(BoardObj.GetTile(0));
            Assert.AreEqual(BoardObj.GetTile(0).TileVal, 2);
            Assert.IsTrue(TileAdded);
        }

        [TestMethod]
        public void AddTile_SetLoc_HighLoc()
        {
            GameManager ManagerObj;
            Board BoardObj;
            int TileLoc;
            bool TileAdded;

            TileLoc = 16;
            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            TileAdded = BoardObj.AddTile(TileLoc);

            Assert.IsFalse(TileAdded);
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 16);
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(TileLoc));
            Assert.IsNull(BoardObj.GetTile(0));
        }

        [TestMethod]
        public void AddTile_SetLoc_LowLoc()
        {
            GameManager ManagerObj;
            Board BoardObj;
            int TileLoc;
            bool TileAdded;

            TileLoc = -1;
            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            TileAdded = BoardObj.AddTile(TileLoc);

            Assert.IsFalse(TileAdded);
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 16);
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(TileLoc));
            Assert.IsNull(BoardObj.GetTile(0));
        }

        [TestMethod]
        public void AddTile_Rand10()
        {
            Random rand;
            GameManager ManagerObj;
            Board BoardObj;
            bool TileAdded;

            rand = new Random(10);
            ManagerObj = new GameManager(false, rand);
            BoardObj = ManagerObj.BoardObj;
            TileAdded = BoardObj.AddTile();

            Assert.IsTrue(TileAdded);
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(15));
            Assert.IsNotNull(BoardObj.GetTile(15));
            Assert.AreEqual(BoardObj.GetTile(15).TileVal, 2);
        }

        [TestMethod]
        public void MoveTile_Up()
        {
            GameManager ManagerObj;
            Board BoardObj;
            Random rand;

            rand = new Random(10);
            ManagerObj = new GameManager(false,rand);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile();
            BoardObj.MoveTiles("up");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at the new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(3));
            Assert.IsNotNull(BoardObj.GetTile(3));
            Assert.AreEqual(BoardObj.GetTile(3).TileVal, 2);
            //Checks if tile was removed from the old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(15));
            Assert.IsNull(BoardObj.GetTile(15));
        }

        [TestMethod]
        public void MoveTile_Left()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(3);
            BoardObj.MoveTiles("left");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNotNull(BoardObj.GetTile(0));
            Assert.AreEqual(BoardObj.GetTile(0).TileVal, 2);
            //Checks that tile was removed from old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(3));
            Assert.IsNull(BoardObj.GetTile(3));
        }

        [TestMethod]
        public void MoveTile_Right(){
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(0);
            BoardObj.MoveTiles("right");
            
            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(3));
            Assert.IsNotNull(BoardObj.GetTile(3));
            Assert.AreEqual(BoardObj.GetTile(3).TileVal, 2);
            //Checks that tile was removed from old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNull(BoardObj.GetTile(0));
        }

        [TestMethod]
        public void MoveTile_Down()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(0);
            BoardObj.MoveTiles("down");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(12));
            Assert.IsNotNull(BoardObj.GetTile(12));
            Assert.AreEqual(BoardObj.GetTile(12).TileVal, 2);
            //Checks that tile was removed from old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNull(BoardObj.GetTile(0));
        }

        [TestMethod]
        public void MoveTile_WrongInput()
        {
            GameManager ManagerObj;
            Board BoardObj;
            bool TileMoved;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(0);
            TileMoved = BoardObj.MoveTiles("dog");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            Assert.IsFalse(TileMoved);
            //Checks that tile is still at old spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNotNull(BoardObj.GetTile(0));
            Assert.AreEqual(BoardObj.GetTile(0).TileVal, 2);
        }

        [TestMethod]
        public void MoveTile_MergeUp()
        {
            GameManager ManagerObj;
            Board BoardObj;
            Random rand;

            rand = new Random(10);
            ManagerObj = new GameManager(false, rand);
            BoardObj = ManagerObj.BoardObj;
            //Random(10) puts tile at 15
            BoardObj.AddTile();
            BoardObj.AddTile(7);
            BoardObj.MoveTiles("up");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at the new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(3));
            Assert.IsNotNull(BoardObj.GetTile(3));
            Assert.AreEqual(BoardObj.GetTile(3).TileVal, 4);
            //Checks if tile was removed from the old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(15));
            Assert.IsNull(BoardObj.GetTile(15));
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(7));
            Assert.IsNull(BoardObj.GetTile(7));
        }

        [TestMethod]
        public void MergeTile_MergeRight()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(0);
            BoardObj.AddTile(1);
            BoardObj.MoveTiles("right");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at the new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(3));
            Assert.IsNotNull(BoardObj.GetTile(3));
            Assert.AreEqual(BoardObj.GetTile(3).TileVal, 4);
            //Checks if tile was removed from the old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNull(BoardObj.GetTile(0));
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(1));
            Assert.IsNull(BoardObj.GetTile(1));
        }

        [TestMethod]
        public void MergeTile_MergeLeft()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(0);
            BoardObj.AddTile(1);
            BoardObj.MoveTiles("left");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at the new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNotNull(BoardObj.GetTile(0));
            Assert.AreEqual(BoardObj.GetTile(0).TileVal, 4);
            //Checks if tile was removed from the old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(1));
            Assert.IsNull(BoardObj.GetTile(1));
        }

        [TestMethod]
        public void MoveTile_MergeDown()
        {
            GameManager ManagerObj;
            Board BoardObj;

            ManagerObj = new GameManager(false);
            BoardObj = ManagerObj.BoardObj;
            BoardObj.AddTile(3);
            BoardObj.AddTile(7);
            BoardObj.MoveTiles("down");

            //Checks if tile count is correct
            Assert.AreEqual(BoardObj.EmptyTiles.Count, 15);
            //Checks if tile is at the new spot
            Assert.IsFalse(BoardObj.EmptyTiles.Contains(15));
            Assert.IsNotNull(BoardObj.GetTile(15));
            Assert.AreEqual(BoardObj.GetTile(15).TileVal, 4);
            //Checks if tile was removed from the old spot
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(0));
            Assert.IsNull(BoardObj.GetTile(0));
            Assert.IsTrue(BoardObj.EmptyTiles.Contains(7));
            Assert.IsNull(BoardObj.GetTile(7));
        }
    }
}
