using System;
using System.IO;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Markup;

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
            //var notificationSound = new SoundPlayer(Properties.Resources.Nice_Meme);
            //notificationSound.PlaySync();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Closing += new CancelEventHandler(MainWindow_Closing);

        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadExternalXaml();
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            SaveExternalXaml();
        }


        public void LoadExternalXaml()
        {
            if (File.Exists(@"C:\Test.xaml"))
            {
                using (FileStream stream = new FileStream(@"C:\Test.xaml", FileMode.Open))
                {
                    this.Content = XamlReader.Load(stream);
                }
            }
        }

        public void SaveExternalXaml()
        {
            using (FileStream stream = new FileStream(@"C:\Test.xaml", FileMode.Create))
            {
                XamlWriter.Save(this.Content, stream);
            }
        }







    }
}