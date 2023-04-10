using System.Collections;
using System.Windows.Media;

namespace _2048
{
    public class Tile
    {
        Hashtable TileColor = new Hashtable()
        {
            {2, Color.FromRgb(238,228,218)},
            {4, Color.FromRgb(237,224,205) },
            {8, Color.FromRgb(242,177,121)},
            {16, Color.FromRgb(245,149,99)},
            {32, Color.FromRgb(246,124,95) },
            {64, Color.FromRgb(246,94,59) },
            {128, Color.FromRgb(237,204,114) },
            {256, Color.FromRgb(237,204,97)},
            {512, Color.FromRgb(237,197,63)},
            {1024, Color.FromRgb(237,194,45) },
            {2048, Color.FromRgb(237,224,200)},
            {4096, Color.FromRgb(0,0,255)},
            {8192, Color.FromRgb(0,0,237)},
            {16384, Color.FromRgb(0,0,219)},
            {32768, Color.FromRgb(0,0,201)},
            {65536, Color.FromRgb(0,0,183)},
            {131072, Color.FromRgb(0,0,165)}

        };
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
            HasCombined= false;
        }
        public void combine(Tile otherTile)
        {
            TileVal = TileVal + otherTile.TileVal;
            HasCombined = true;
        }
        public Color GetColor()
        {
            return (Color)TileColor[TileVal];
        }
    }
}
