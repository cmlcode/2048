﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using _2048;

namespace _2048Board
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
            //Console.WriteLine("Loaded");
            ManagerObj = new GameManager(false);
            ScoreDisplay = this.FindName("ScoreLabel") as Label;
            ScoreDisplay.Content = "Score: " + ManagerObj.Score+ "\tHigh Score: "+ ManagerObj.HighScore;

        }
        void WindowRendered(object sender, EventArgs e)
        {
            FillTiles();
            //Console.WriteLine("Rendered");
            GameLoop();
        }

        public void EndGame()
        {
            string EndText;

            EndText = "GAME OVER\nScore = " + ManagerObj.Score;

            if (MessageBox.Show(EndText,"Play Again?",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ManagerObj.ResetGame();
                ScoreDisplay.Content = "Score: 0\tHigh Score: " + ManagerObj.HighScore;
                FillTiles();
                GameLoop();
                return;
            }
        }

        public void GameLoop()
        { 
            ManagerObj.BoardObj.AddTile();
            FillTiles();
            if (ManagerObj.HasWinner())
            {
                EndGame();
                return;
            }
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

                ScoreDisplay.Content = "Score: " + ManagerObj.Score + "\tHigh Score: " + ManagerObj.HighScore;
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
                    ((Label)GameGrid.Children[TileCount]).BorderBrush = new SolidColorBrush(Colors.Black);
                    ((Label)GameGrid.Children[TileCount]).Content = Tiles[TileCount].TileVal.ToString();
                }
                
            }
        }

    }
}
