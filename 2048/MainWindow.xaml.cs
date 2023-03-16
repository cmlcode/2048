using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameManager gameObj;
        
        
        public MainWindow()
        {
            InitializeComponent();
            gameObj = new GameManager(false);
            Label ScoreLabel = this.FindName("ScoreLabel") as Label;
            ScoreLabel.Content = "Score: "+gameObj.Score; 
        }

        

        public void GameLoop()
        {
            CreateTile();
            HasWinner();
            MoveTiles();
        }

        public void CreateTile()
        {
            return;
        }

        public bool HasWinner()
        {
            return false;
        }

        public void MoveTiles()
        {
            for(int TileLoc=0;TileLoc < 16; TileLoc++) {
            }
        }

    }
}
