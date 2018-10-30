using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
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


namespace memorygame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int cols = 5;
        private const int rows = 4;
        MemoryGrid grid;
        
        
        

        public MainWindow()
        {
            InitializeComponent();
            grid = new MemoryGrid(GameGrid, cols, rows);
            Closing += new CancelEventHandler(MainWindow_Closing);
        }






        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Save();
        }




        public void Save()
        {
            string score = grid.GetScore() + Environment.NewLine;
            string score1 = grid.GetScore1() + Environment.NewLine;
            string turnCount = grid.GetTurnCount() + Environment.NewLine;

            File.WriteAllText("memory.sav", score);
            File.AppendAllText("memory.sav", score1);
            File.AppendAllText("memory.sav", turnCount);
        }

    }   
}