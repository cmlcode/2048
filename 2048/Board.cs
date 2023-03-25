using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace _2048
{
    public class Board
    {
        private List<Tile> _tiles;
        public List<Tile> Tiles
        {
            get { return _tiles; }
            private set { _tiles = value; }
        }
        private HashSet<int> _emptyTiles;
        public HashSet<int> EmptyTiles
        {
            get { return _emptyTiles; }
            private set { _emptyTiles = value; }
        }
        public readonly Random rnd;
        private GameManager Manager;

        public Board(GameManager ManagerObj)
        {
            EmptyTiles = new HashSet<int>
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
            };
            Tiles = new List<Tile>(new Tile[16]);
            rnd = new Random();
            Manager = ManagerObj;
        }

        public Board(GameManager ManagerObj, Random rnd)
        {
            EmptyTiles = new HashSet<int>
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
            };
            Tiles = new List<Tile>(new Tile[16]);
            this.rnd = rnd;
            Manager = ManagerObj;
        }

        public bool AddTile()
        {
            int TileLoc = -1;

            while (TileLoc > 15 || TileLoc < 0)
            {
                TileLoc = EmptyTiles.ElementAt(rnd.Next(EmptyTiles.Count()));
            }

            return AddTile(TileLoc);
        }
        public bool AddTile(int TileLoc)
        {
            if (TileLoc > 15 || TileLoc < 0) return false;
            EmptyTiles.Remove(TileLoc);
            Tiles[TileLoc] = new Tile(2);
            return true;
        }

        public string GetBoard()
        {
            string output = "";

            for(int TileIndex = 0; TileIndex < Tiles.Count; TileIndex++)
            {
                if(Tiles[TileIndex] == null)
                {
                    output += "---- | ";
                }
                else
                {
                    output += Tiles[TileIndex].TileVal.ToString().PadLeft(4,'0')+" | ";
                }
                if (TileIndex % 4 == 3)
                {
                    output += "\n";
                }
            }

            return output;
        }

        public bool MoveTiles(string direction)
        {
            int movement = GetMovement(direction);
            Func<int, bool> HitEdge;


            switch (movement)
            {
                case -4:
                    HitEdge = NewFileLoc => NewFileLoc < 4;
                    break;
                case -1: 
                    HitEdge = NewFileLoc => NewFileLoc%4 == 0;
                    break;
                case 1:
                    HitEdge = NewFileLoc => (NewFileLoc % 4) == 3;
                    break;
                case 4:
                    HitEdge = NewFileLoc => NewFileLoc > 11;
                    break;
                    
                default:
                    Console.WriteLine("ERROR: Direction given not allowed");
                    return false;
            }
            return MoveDirection(movement, HitEdge);
        }

        private int GetMovement(string direction)
        {
            if (direction == "up") return -4;
            else if (direction == "right") return 1;
            else if (direction == "left") return -1;
            else if (direction == "down") return 4;
            return 0;
        }

        private bool MoveDirection(int movement, Func<int, bool>HitEdge)
        {
            int StarterVal;
            Func <int,bool> EndCondition;
            int IndexMovement;
            bool TileMoved = false;

            if (movement < 0)
            {
                StarterVal = 0;
                EndCondition = CurrentIndex => CurrentIndex<Tiles.Count;
                IndexMovement = 1;
            }
            else
            {
                StarterVal = Tiles.Count - 1;
                EndCondition = CurrentIndex => CurrentIndex >= 0;
                IndexMovement = -1;
            }
            for (int fileIndex = StarterVal; EndCondition(fileIndex); fileIndex += IndexMovement)
            {
                if (Tiles[fileIndex] == null)
                {
                    continue;
                }
                int tempIndex = fileIndex;
                Tiles[tempIndex].HasCombined = false;
                while(!HitEdge(tempIndex) && Tiles[tempIndex + movement] == null)
                {
                    MoveATile(movement, tempIndex);
                    tempIndex += movement;
                    TileMoved = true;
                }
                if (!HitEdge(tempIndex) && Tiles[tempIndex + movement].TileVal == Tiles[tempIndex].TileVal)
                {
                    if (!Tiles[tempIndex + movement].HasCombined)
                    {
                        Tiles[tempIndex + movement].combine(Tiles[tempIndex]);
                        RemoveTile(tempIndex);
                        TileMoved = true;
                        Manager.IncreaseScore((int)(Tiles[tempIndex + movement].TileVal * 2));
                    }   
                }
            }
            return TileMoved;
        }
        private void RemoveTile(int TileIndex)
        {
            EmptyTiles.Add(TileIndex);
            Tiles[TileIndex] = null;
        }
        private void MoveATile(int movement, int tileIndex)
        {
            EmptyTiles.Remove(tileIndex + movement);
            Tiles[tileIndex + movement] = Tiles[tileIndex];
            RemoveTile(tileIndex);
        }

        public Tile GetTile(int TileLoc)
        {
            return Tiles[TileLoc];
        }
        
        public bool BoardFull()
        {
            return EmptyTiles.Count == 0;
        }
    }
}
