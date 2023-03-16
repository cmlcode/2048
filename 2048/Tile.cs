using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Tile
    {
        private int? _tileVal;
        public int? TileVal{
            get {
                return _tileVal;
            }
            private set
            {
                _tileVal = value;
            }
        }
        public bool HasCombined;

        public Tile(int? NewTileValue)
        {
            TileVal = NewTileValue;
            HasCombined = false;
        }
        public Tile()
        {
            _tileVal = null;
        }
        public void combine(Tile otherTile)
        {
            this.TileVal = this.TileVal + otherTile.TileVal;
            this.HasCombined = true;
        }
    }
}
