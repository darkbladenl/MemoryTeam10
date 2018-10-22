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
using System.Windows.Shapes;

namespace memorygame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public string Gamemode;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Naambox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Inhoud = Naambox.Text;

            if (Inhoud != "") 
            {
                BtnSpelen.IsEnabled = true;
            }
        }

        private void Scores_Click(object sender, RoutedEventArgs e)
        {
            Gamemode = "Scores";
            Scores.Background = Scores.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.Red;
            Timer.Background = Timer.Background == Brushes.LightGray ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.LightGray;

        }

        private void Timer_Click(object sender, RoutedEventArgs e)
        {
            Gamemode = "Timer";
            Timer.Background = Timer.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.Red;
            Scores.Background = Scores.Background == Brushes.LightGray ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.LightGray;

        }
    }
}
