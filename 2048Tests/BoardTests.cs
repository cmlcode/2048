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
    }
}
