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
        //lijst met kaartjes 1 t/m 8 x2
        private List<int> cards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };
        //lijst met kaartjes die nu omgedraaid zijn
        private List<double> openCards = new List<double>();
        // lijst met kaartjes die al eens gezien zijn
        private List<Image> seenCards = new List<Image>();
        private List<int> openCardsIndex = new List<int>();
        private List<ImageSource> openCardsSources = new List<ImageSource>();
        int score = 0;//score
        int index = 0;
        Label scoreboard = new Label();//scorebord
        Label card0 = new Label();//kaartnummer
        Label card1 = new Label();//kaartnummer
        Label card2 = new Label();//kaartnummer
        Label card3 = new Label();//kaartnummer
        Label card4 = new Label();//kaartnummer
        Label card5 = new Label();//kaartnummer
        Label card6 = new Label();//kaartnummer
        Label card7 = new Label();//kaartnummer
        Label card8 = new Label();//kaartnummer
        Label card9 = new Label();//kaartnummer
        Label card10 = new Label();//kaartnummer
        Label card11 = new Label();//kaartnummer
        Label card12 = new Label();//kaartnummer
        Label card13 = new Label();//kaartnummer
        Label card14 = new Label();//kaartnummer
        Label card15 = new Label();//kaartnummer

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
            AddScoreboard();
           
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
        private void AddScoreboard()
        {               
            scoreboard.Content = score;            
            scoreboard.FontSize = 30;
            scoreboard.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(scoreboard, 0);
            Grid.SetColumn(scoreboard, 5);
            grid.Children.Add(scoreboard);                                                           
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
        async Task PutTaskDelay()
        {
            await Task.Delay(1000);
        }
       
        private async void CardClick(object sender, MouseButtonEventArgs e)
        {
            
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
            
            openCards.Add(front.Height);
            seenCards.Add(card);
            openCardsIndex.Add(grid.Children.IndexOf(card));
            openCardsSources.Add(card.Source);
            if (openCards.Count == 2)
            {                              
                if (openCards[0] == openCards[1])
                {
                    score++;
                    await PutTaskDelay();
                    //grid.Children.RemoveAt(openCardsIndex[0]);
                    //card.Source = null;
                    //grid.Children.Insert((openCardsIndex[0] - 1), test);
                }
                if (!(openCards[0] == openCards[1]))
                {
                    score--;
                    await PutTaskDelay();
                    card.Source = new BitmapImage(new Uri("Resources/Images_Rear/DC_Comics_logo.png", UriKind.Relative));
                    seenCards[0].Source = new BitmapImage(new Uri("Resources/Images_Rear/DC_Comics_logo.png", UriKind.Relative)); 




                }
                seenCards.RemoveRange(0, 2);
                openCards.RemoveRange(0, 2);
                scoreboard.Content = score;
                openCardsIndex.RemoveRange(0, 2);
            }           
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
