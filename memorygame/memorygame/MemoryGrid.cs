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
            //AddIndex0();
            /*AddIndex1();
            AddIndex2();
            AddIndex3();
            AddIndex4();
            AddIndex5();
            AddIndex6();
            AddIndex7();
            AddIndex8();
            AddIndex9();
            AddIndex10();
            AddIndex11();
            AddIndex12();
            AddIndex13();
            AddIndex14();
            AddIndex15();*/
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

        /*private void AddIndex0()
        {
            
            card0.Content = 0;
            card0.Width = 1;
            card0.Height = 1;
            Grid.SetColumn(card0, 0);
            grid.Children.Add(card0);
               
        }
        private void AddIndex1()
        {

            card1.Content = 1;
            card1.Width = 1;
            card1.Height = 1;
            Grid.SetColumn(card1, 1);
            grid.Children.Add(card1);

        }
        private void AddIndex2()
        {

            card2.Content = 2;
            card2.Width = 1;
            card2.Height = 1;
            Grid.SetColumn(card2, 2);
            grid.Children.Add(card2);

        }
        private void AddIndex3()
        {

            card3.Content = 3;
            card3.Width = 1;
            card3.Height = 1;
            Grid.SetColumn(card3, 3);
            grid.Children.Add(card3);

        }
        private void AddIndex4()
        {

            card4.Content = 4;
            card4.Width = 1;
            card4.Height = 1;
            Grid.SetColumn(card4, 0);
            Grid.SetRow(card4, 1);
            grid.Children.Add(card4);

        }
        private void AddIndex5()
        {

            card5.Content = 5;
            card5.Width = 1;
            card5.Height = 1;
            Grid.SetColumn(card5, 1);
            Grid.SetRow(card5, 1);
            grid.Children.Add(card5);

        }
        private void AddIndex6()
        {

            card6.Content = 6;
            card5.Width = 1;
            card6.Height = 1;
            Grid.SetColumn(card6, 2);
            Grid.SetRow(card6, 1);
            grid.Children.Add(card6);

        }
        private void AddIndex7()
        {

            card7.Content = 7;
            card7.Width = 1;
            card7.Height = 1;
            Grid.SetColumn(card7, 3);
            Grid.SetRow(card7, 1);
            grid.Children.Add(card7);

        }
        private void AddIndex8()
        {

            card8.Content = 8;
            card8.Width = 1;
            card8.Height = 1;
            Grid.SetColumn(card8, 0);
            Grid.SetRow(card8, 2);
            grid.Children.Add(card8);

        }
        private void AddIndex9()
        {

            card9.Content = 9;
            card9.Width = 1;
            card9.Height = 1;
            Grid.SetColumn(card9, 1);
            Grid.SetRow(card9, 2);
            grid.Children.Add(card9);

        }
        private void AddIndex10()
        {

            card10.Content = 10;
            card10.Width = 1;
            card10.Height = 1;
            Grid.SetColumn(card10, 2);
            Grid.SetRow(card10, 2);
            grid.Children.Add(card10);

        }
        private void AddIndex11()
        {

            card11.Content = 11;
            card11.Width = 1;
            card11.Height = 1;
            Grid.SetColumn(card11, 3);
            Grid.SetRow(card11, 2);
            grid.Children.Add(card11);

        }
        private void AddIndex12()
        {

            card12.Content = 12;
            card12.Width = 1;
            card12.Height = 1;
            Grid.SetColumn(card12, 0);
            Grid.SetRow(card12, 3);
            grid.Children.Add(card12);

        }
        private void AddIndex13()
        {

            card13.Content = 13;
            card13.Width = 1;
            card13.Height = 1;
            Grid.SetColumn(card13, 1);
            Grid.SetRow(card13, 3);
            grid.Children.Add(card13);

        }
        private void AddIndex14()
        {

            card14.Content = 14;
            card14.Width = 1;
            card14.Height = 1;
            Grid.SetColumn(card14, 2);
            Grid.SetRow(card14, 3);
            grid.Children.Add(card14);

        }
        private void AddIndex15()
        {

            card15.Content = 15;
            card15.Width = 1;
            card15.Height = 1;
            Grid.SetColumn(card15, 3);
            Grid.SetRow(card15, 3);
            grid.Children.Add(card15);

        }*/

    }
}
