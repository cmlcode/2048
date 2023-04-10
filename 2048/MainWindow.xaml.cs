using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _2048
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsPlaying = false;
        readonly Hashtable InputConversion = new Hashtable()
        {
            {"Left", ConsoleKey.LeftArrow},
            {"Up", ConsoleKey.UpArrow},
            {"Right", ConsoleKey.RightArrow },
            {"Down", ConsoleKey.DownArrow}
        };
        GameManager ManagerObj;
        Label ScoreDisplay;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Loaded");
            ManagerObj = new GameManager(false);
            ScoreDisplay = this.FindName("ScoreLabel") as Label;
            ScoreDisplay.Content = "Score: " + ManagerObj.Score;

        }
        void WindowRendered(object sender, EventArgs e)
        {
            FillTiles();
            Console.WriteLine("Rendered");
            GameLoop();
        }
        public void GameLoop()
        { 
            ManagerObj.BoardObj.AddTile();
            if (ManagerObj.HasWinner())
            {
                MessageBox.Show("Game Over");
                return;
            }
            FillTiles();
            IsPlaying = true;
        }
            


        public bool HasWinner()
        {
            return false;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!IsPlaying)
            {
                return;
            }
            if (!InputConversion.ContainsKey(e.Key.ToString()))
            {
                return;
            }
            ConsoleKey InputKey = (ConsoleKey)InputConversion[e.Key.ToString()];
            if (ManagerObj.MoveTiles(InputKey))
            {
                e.Handled = true;
                IsPlaying = false;

                ScoreDisplay.Content = "Score: " + ManagerObj.Score;
                GameLoop();
            }
            else
            {
                e.Handled = true;
            }
        }

        public void FillTiles()
        {
            List<Tile> Tiles = ManagerObj.BoardObj.Tiles;
            int RowCount = 0;
            int ColCount = 0;
            for (int TileCount = 0; TileCount < Tiles.Count; TileCount++)
            {
                if (ColCount == 4)
                {
                    RowCount += 1;
                    ColCount = 0;
                }
                if (Tiles[TileCount] == null) {
                    ((Label)GameGrid.Children[TileCount]).Background = new SolidColorBrush(Colors.Beige);
                    ((Label)GameGrid.Children[TileCount]).Content = "";
                }
                else
                {
                    ((Label)GameGrid.Children[TileCount]).Background = new SolidColorBrush(Tiles[TileCount].GetColor());
                    ((Label)GameGrid.Children[TileCount]).Content = Tiles[TileCount].TileVal.ToString();
                }
                
            }
        }

    }
}
