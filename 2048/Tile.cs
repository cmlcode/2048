using System.Collections;
using System.Windows.Media;

namespace _2048
{
    public class Tile
    {
        //Gets the color of the tile based on its value
        Hashtable TileColor = new Hashtable()
        {
            {2, Color.FromRgb(238,228,218)},
            {4, Color.FromRgb(255,197,165) },
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
        
        //Value displayed on the cell
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

        //Whether the cell has already combined with another cell in a round of movement
        public bool HasCombined;

        public Tile()
        {
            //Creates a null tile
            _tileVal = null;
            HasCombined = false;
        }

        public Tile(int NewTileValue)
        {
            //Creates a tile with a set value
            TileVal = NewTileValue;
            HasCombined = false;
        }
        
        public void combine(Tile otherTile)
        {
            //Combines a tile with the other tile
            TileVal = TileVal + otherTile.TileVal;
            HasCombined = true;
        }
        public Color GetColor()
        {
            //Gets the color of the cell from the hashtable
            return (Color)TileColor[TileVal];
        }
    }
}
