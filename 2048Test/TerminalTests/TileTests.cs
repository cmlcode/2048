﻿using _2048;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TerminalTests
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void Constructor_NoVal()
        {
            Tile tile;

            tile = new Tile();

            Assert.IsNull(tile.TileVal);
            Assert.IsFalse(tile.HasCombined);
        }

        [TestMethod]
        public void Constructor_ProvidedVal()
        {
            Tile tile;

            tile = new Tile(2);

            Assert.AreEqual(2, tile.TileVal);
            Assert.IsFalse(tile.HasCombined);
        }

        [TestMethod]
        public void Combine()
        {
            Tile TileObj;
            Tile OtherTileObj;

            TileObj = new Tile(2);
            OtherTileObj = new Tile(2);
            TileObj.combine(OtherTileObj);

            Assert.AreEqual(TileObj.TileVal, 4);
            Assert.IsTrue(TileObj.HasCombined);
        }
    }
}