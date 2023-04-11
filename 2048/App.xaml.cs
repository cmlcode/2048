using System;
using System.Runtime.InteropServices;
using System.Windows;
using _2048;

namespace _2048Board
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        protected override void OnStartup(StartupEventArgs e)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            base.OnStartup(e);
            string[] args = e.Args;
            if (args .Length == 0) {
                
                var window = new MainWindow();
                window.ShowDialog();
                return;
            }
            foreach (string arg in args){
                if (arg == "-t")
                {
                    ShowWindow(handle, SW_SHOW);
                    Console.WriteLine("Terminal args");
                    GameManager gameObj = new GameManager(true);
                    return;
                }
            }
        }

        
    }
}
