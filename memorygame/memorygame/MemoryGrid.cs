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
        // een grid
        private Grid grid;
        private const int cols = 4;
        private const int rows = 4;
        //lijst met kaartjes 1 t/m 8 x2
        private List<int> cards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };
        //lijst met kaartjes 1 t/m 8 x2 voor reset functie
        private List<int> newCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8 };
        //lijst met kaartjes die nu omgedraaid zijn
        private List<double> openCards = new List<double>();
        //lijst met kaartjes die al eens gezien zijn
        private List<Image> seenCards = new List<Image>();
        //lijst met kaartjes die opgelost zijn
        private List<Image> SolvedCards = new List<Image>();

        int score = 0;//score
        int score1 = 0;

        Label scoreboard = new Label();//scorebord
        Label scoreboard1 = new Label();
        Button resetBtn = new Button();//resetknop
        Button quitBtn = new Button();//sluitknop
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
            AddScoreboard1();
            AddResetBtn();
        }
        //METHODEN

        //SPEELBORD
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

        //KAARTEN ACHTERKANT
        /// <summary>
        /// voegt images toe, klikbaar
        /// </summary>
        private void AddCards()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
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

        //KAARTEN VOORKANT
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
                //en lager dan (cards.count){hoeveel items erin de lijst staan}
                int index = rnd.Next(cards.Count);
                int imageNR = cards[index];//de ImageNR wordt cards[index] een random item{een getal} uit de lijst cards
                cards.RemoveAt(index);//het item wordt verwijdert uit de lijst zodat deze niet nog een keer gepakt kan worden
                ImageSource source = new BitmapImage(new Uri("Resources/Images_Front/" + imageNR + ".png", UriKind.Relative));
                images.Add(source);

            }
            return images;
        }

        //MUISKLIK
        /// <summary>
        /// zorgt voor een delay
        /// </summary>
        /// <returns></returns>
        async Task PutTaskDelay()
        {
            await Task.Delay(600);
        }

        private async void CardClick(object sender, MouseButtonEventArgs e)
        {

            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;


            openCards.Add(front.Height);
            seenCards.Add(card);



            if (openCards.Count == 2)
            {
                if (openCards[0] == openCards[1])
                {
                    SolvedCards.Add(card);

                    if (scoreboard1.Background == null)
                    {
                        score = score + 100;
                        scoreboard.Content = "Player1: \n" + score;
                    }
                    else
                    {
                        score1 = score1 + 100;
                        scoreboard1.Content = "Player2: \n" + score1;
                    }
                }
                if (!(openCards[0] == openCards[1]))
                {
                    await PutTaskDelay();
                    card.Source = new BitmapImage(new Uri("Resources/Images_Rear/DC_Comics_logo.png", UriKind.Relative));
                    seenCards[0].Source = new BitmapImage(new Uri("Resources/Images_Rear/DC_Comics_logo.png", UriKind.Relative));

                    if (scoreboard1.Background == null)
                    {
                        scoreboard1.Background = new SolidColorBrush(Color.FromRgb(2, 119, 243));
                        scoreboard.Background = null;

                    }
                    else
                    {
                        scoreboard.Background = new SolidColorBrush(Color.FromRgb(2, 119, 243));
                        scoreboard1.Background = null;

                    }
                }
                seenCards.RemoveRange(0, 2);
                openCards.RemoveRange(0, 2);
            }
            if (SolvedCards.Count == 8)
            {
                if (score > score1)
                {
                    MessageBox.Show("Player1 WIN");
                }

                if (score < score1)
                {
                    MessageBox.Show("Player2 WIN");
                }

                if (score == score1)
                {
                    MessageBox.Show("TIE");
                }

            }
        }


        //SCOREBORD
        /// <summary>
        /// voegt een label toe
        /// </summary>
        private void AddScoreboard()
        {
            scoreboard.Content = "Player1: \n" + score;
            scoreboard.FontSize = 40;
            scoreboard.Background = new SolidColorBrush(Color.FromRgb(2, 119, 243));
            scoreboard.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(scoreboard, 0);
            Grid.SetColumn(scoreboard, 5);
            grid.Children.Add(scoreboard);
        }

        //SCOREBORD1
        private void AddScoreboard1()
        {
            scoreboard1.Content = "Player2: \n" + score;
            scoreboard1.FontSize = 40;
            scoreboard1.Background = null;
            scoreboard1.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(scoreboard1, 1);
            Grid.SetColumn(scoreboard1, 5);
            grid.Children.Add(scoreboard1);
        }

        //RESETKNOP
        private void AddResetBtn()
        {
            resetBtn.Content = "RESET";
            resetBtn.FontSize = 30;
            resetBtn.Foreground = Brushes.White;
            resetBtn.Background = new SolidColorBrush(Color.FromRgb(2, 119, 243));
            resetBtn.Height = 50;
            resetBtn.Click += ResetGame;
            Grid.SetRow(resetBtn, 2);
            Grid.SetColumn(resetBtn, 5);
            grid.Children.Add(resetBtn);
        }
        private void ResetGame(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();

            SolvedCards.Clear();
            openCards.Clear();
            seenCards.Clear();


            cards.AddRange(newCards);
            grid.Children.Add(scoreboard);
            grid.Children.Add(scoreboard1);
            scoreboard.Background = new SolidColorBrush(Color.FromRgb(2, 119, 243));
            scoreboard1.Background = null;
            score = 0;
            score1 = 0;
            scoreboard.Content = "Player1: \n" + score;
            scoreboard1.Content = "Player2: \n" + score1;
            grid.Children.Add(resetBtn);
            AddCards();

        }
    }
}
