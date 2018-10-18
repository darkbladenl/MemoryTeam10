using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace memorygame
{
    public class MemoryGrid
    {   
        //ATTRIBUTEN
        private Grid grid;
        private const int cols = 4;
        private const int rows = 4;
        //nieuwe instantie van lijst met kaartjes 1 t/m 8 x2
        private List<int> cards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };
        private int score = 0;
        private int omgedraaid = 0; // aantal omgedraaide kaarten
        //CONSTRUCTORS
        /// <summary>
        /// MemoryGrid bestaat uit een grid met rijen en kolommen, met daarin: Images en Labels
        /// </summary>
        /// <param name="grid">de grid</param>
        /// <param name="cols">kolommen</param>
        /// <param name="rows">rijen</param>

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            InitializeGameGrid(cols, rows);
            AddCards();
            AddLabel();
            
        }         
        //METHODEN
        /// <summary>
        /// maakt een speelbord aan met aantal kolommen en rijen
        /// </summary>
        /// <param name="cols">het aantal kolommen</param>
        /// <param name="rows">het aantal rijen</param>
        private void InitializeGameGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
               grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
               grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        /// <summary>
        /// voegt een label toe
        /// </summary>
        public void AddLabel()
        {
            while (true)
            {
                Label title = new Label();
                title.Content = score;
                title.FontFamily = new FontFamily("batman_font/#BatmanForeverAlternate");
                title.FontSize = 30;
                title.HorizontalAlignment = HorizontalAlignment.Center;

                Grid.SetColumn(title, 5);
                grid.Children.Add(title);
            }
        }
        /// <summary>
        /// voegt images toe, klikbaar
        /// </summary>
        private void AddCards()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for(int column = 0; column < cols; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Resources/Images_Rear/DC_Comics_logo.png", UriKind.Relative));
                    backgroundImage.Tag = images.First();
                    images.RemoveAt(0);
                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);
                }
            }
        }
        
        private void CardClick(object sender, MouseButtonEventArgs e)
        {           
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
            score++;
            omgedraaid++;
            MessageBox.Show (score.ToString());
        }

        /// <summary>
        /// plaatst images in een willekeurige volgorde
        /// </summary>
        /// <returns>return een lijst met images</returns>
        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            
            // rnd geeft een willekeurig getal terug
            Random rnd = new Random();

            for (int i = 0; i < 16; i++)
            {
                //index is gelijk aan rnd{willekeurig getal} die Next{niet negatief} is 
                //en lager dan (cardNR.count){hoeveel items erin de lijst staan
                int index = rnd.Next(cards.Count);
                int imageNR = cards[index];//de ImageNR wordt cardNR[index] een random item{een getal} uit de lijst cardNR
                cards.RemoveAt(index);//het item wordt verwijdert uit de lijst zodat deze niet nog een keer gepakt kan worden
                ImageSource source = new BitmapImage(new Uri("Resources/Images_Front/" + imageNR + ".png", UriKind.Relative));
                images.Add(source);
            }
            return images; 
        }

        
       
        
    }
}
