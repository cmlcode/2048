using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace _2048
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string[] args = e.Args;
            if (args .Length == 0) {
                Console.WriteLine("No args");
                var window = new MainWindow();
                window.ShowDialog();
                return;
            }
            foreach (string arg in args){
                if (arg == "-t")
                {
                    Console.WriteLine("Terminal args");
                    AllocConsole();
                    GameManager gameObj = new GameManager(true);
                    return;
                }
            }
        }

        [DllImport("Kernel32.dll")]
        static extern void AllocConsole();
    }
}
