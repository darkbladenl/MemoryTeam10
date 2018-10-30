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
        public string Thema;
        public string Naam;
        public string Naam1;

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
            Naam = Naambox.Text.ToString();

            if (ComboBox.Text != "" && Naambox.Text != "" && Gamemode != "" && Naambox1.Text != "")
            {
                BtnSpelen.IsEnabled = true;
            }
        }
        private void Naambox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Naam1 = Naambox.Text.ToString();

            if (ComboBox.Text != "" && Naambox.Text != "" && Gamemode != "" && Naambox1.Text != "")
            {
                BtnSpelen.IsEnabled = true;
            }
        }
        private void Scores_Click(object sender, RoutedEventArgs e)
        {
            Gamemode = "Scores";
            Scores.Background = Scores.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.Red;
            Timer.Background = Timer.Background == Brushes.LightGray ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.LightGray;

            if (ComboBox.Text != "" && Naambox.Text != "" && Gamemode != "" && Naambox1.Text != "")
            {
                BtnSpelen.IsEnabled = true;
            }
        }

        private void Timer_Click(object sender, RoutedEventArgs e)
        {
            Gamemode = "Timer";
            Timer.Background = Timer.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.Red;
            Scores.Background = Scores.Background == Brushes.LightGray ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.LightGray;

            if (ComboBox.Text != "" && Naambox.Text != "" && Gamemode != "" && Naambox1.Text != "")
            {
                BtnSpelen.IsEnabled = true;
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Thema = ComboBox.Text.ToString();
            ///label1.Content = Thema;


            if (ComboBox.Text != "" && Naambox.Text != "" && Gamemode != "" && Naambox1.Text != "")
            {
                BtnSpelen.IsEnabled = true;
            }
        }

      

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            JHtextBlock.Text = "🅱️oey Hovinga";
        }
    }
}
