using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
    public class Board
    {
        //List of all tile objects
        private List<Tile> _tiles;
        public List<Tile> Tiles
        {
            get { return _tiles; }
            private set { _tiles = value; }
        }

        //Contains which cells are empty
        private HashSet<int> _emptyTiles;
        public HashSet<int> EmptyTiles
        {
            get { return _emptyTiles; }
            private set { _emptyTiles = value; }
        }

        //Random value used for tile position generation
        public readonly Random rnd;

        //GameManager that created board instance
        private GameManager Manager;


        public Board(GameManager ManagerObj)
        {
            //Sets tile lists to correct size, gets random value, and sets manager to game manager that created it
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
            //Uses a set logic value, follows same logic as constructor above
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
            //Adds a tile in a random spot on the board
            int TileLoc = -1;

            while (TileLoc > 15 || TileLoc < 0)
            {
                TileLoc = EmptyTiles.ElementAt(rnd.Next(EmptyTiles.Count()));
            }

            return AddTile(TileLoc);
        }
        public bool AddTile(int TileLoc)
        {
            //Adds a tile in a set spot on the board
            if (TileLoc > 15 || TileLoc < 0) return false;
            EmptyTiles.Remove(TileLoc);
            Tiles[TileLoc] = new Tile(2);
            return true;
        }

        public override string ToString()
        {
            //Gets a string version of the board with full formatting

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
            /*
             * Moves the tiles based on direction input
             * Determines how to tell if tile hit an edge, used in MoveDirection call in the function
             * Returns true if able to move, false otherse
            */
               
            //Gets how far the cell will move every movement interation
            int movement = GetMovement(direction);
            Func<int, bool> HitEdge;

            //Determines how to tell if the tile is on the edge
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

            //Moves the tiles in the direction given
            return MoveDirection(movement, HitEdge);
        }

        private int GetMovement(string direction)
        {
            //Changes the input into difference of CurrentTilePos and NewTilePos
            if (direction == "up") return -4;
            else if (direction == "right") return 1;
            else if (direction == "left") return -1;
            else if (direction == "down") return 4;
            return 0;
        }

        private bool MoveDirection(int movement, Func<int, bool>HitEdge)
        {
            /*
             * Moves the tiles until it hits the edge of the board or another tile
             * Returns whether a tile was able to be moved
            */

            /*
             * Values necessary to write for loop
             * StarterVal determines which cell to start on
             * HitLastCell determines which cell for loop ends on
             * IndexMovement determines which direction to iterate over the cells in 
            */
            int StarterVal;
            Func <int,bool> HitLastCell;
            int IndexMovement;

            //Determines whether a tile has moved
            bool TileMoved = false;

            /*
             * If the tiles are moving negatively, have to iterate from first cell (0) to last cell (15)
             * If iterated in other direction, cells would run into unmoved cells
            */
            if (movement < 0)
            {
                StarterVal = 0;
                HitLastCell = CurrentIndex => CurrentIndex<Tiles.Count;
                IndexMovement = 1;
            }
            /*
             * If the tiles are moving negatively, have to iterate from last cell (15) to first cell (0)
             * If iterated in other direction, cells would run into unmoved cells
            */
            else
            {
                StarterVal = Tiles.Count - 1;
                HitLastCell = CurrentIndex => CurrentIndex >= 0;
                IndexMovement = -1;
            }

            //Writes for loop moving in correct direction
            for (int TileIndex = StarterVal; HitLastCell(TileIndex); TileIndex += IndexMovement)
            {
                //If there is no value in the cell, it can't be moved
                if (Tiles[TileIndex] == null)
                {
                    continue;
                }

                int TempIndex = TileIndex;
                Tiles[TempIndex].HasCombined = false;
                //While the tile has not hit an edge and the next tile is empty, move the tile forwards
                while(!HitEdge(TempIndex) && Tiles[TempIndex + movement] == null)
                {
                    MoveATile(movement, TempIndex);
                    TempIndex += movement;
                    TileMoved = true;
                }
                //If the tile hit another tile with the same value, merge the value at other tile's spot
                if (!HitEdge(TempIndex) && Tiles[TempIndex + movement].TileVal == Tiles[TempIndex].TileVal)
                {
                    if (!Tiles[TempIndex + movement].HasCombined)
                    {
                        Tiles[TempIndex + movement].combine(Tiles[TempIndex]);
                        RemoveTile(TempIndex);
                        TileMoved = true;
                        Manager.IncreaseScore((int)(Tiles[TempIndex + movement].TileVal * 2));
                    }   
                }
            }
            return TileMoved;
        }
        private void RemoveTile(int TileIndex)
        {
            //Removes the tile at the input location
            EmptyTiles.Add(TileIndex);
            Tiles[TileIndex] = null;
        }
        private void MoveATile(int movement, int tileIndex)
        {
            //Moves the tile one instace in the direction provided
            EmptyTiles.Remove(tileIndex + movement);
            Tiles[tileIndex + movement] = Tiles[tileIndex];
            RemoveTile(tileIndex);
        }

        public Tile GetTile(int TileLoc)
        {
            //Gets the tile at a location
            return Tiles[TileLoc];
        }
        
        public bool BoardFull()
        {
            //Returns true if there are any empty tiles remaining,
            return EmptyTiles.Count == 0;
        }
    }
}
