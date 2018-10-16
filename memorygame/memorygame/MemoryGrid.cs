using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace memorygame
{
    public class MemoryGrid
    {
        private Grid GameGrid;

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            GameGrid = grid;
            InitializeGameGrid(cols, rows);
            AddLabel();
        }       

        private void InitializeGameGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void AddLabel()
        {
            Label title = new Label();
            title.Content = "Memory";
            title.FontFamily = new FontFamily("Distant Galaxy");
            title.FontSize = 40;
            title.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(title, 1);
            GameGrid.Children.Add(title);
        }

        
    }
}
