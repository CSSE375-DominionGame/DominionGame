using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using DominionGUI.Properties;
using DominionCards;
namespace DominionGUI
{
    public partial class GraphicsBoard : Form, IObserver<GameBoard>
    {
        private static GraphicsBoard instance;
        public DominionCards.GameBoard board;
        private System.Drawing.Bitmap[] imageadd;
        private System.Type[] cardsadd;
        private System.Type[] basiccard;

        private Dictionary<DominionCards.Card, System.Drawing.Bitmap> cardImages;

        private List<IDisposable> unsubscribers;

        private CardButton[] firstRow = new CardButton[7];
        private CardButton[] secondRow = new CardButton[5];
        private CardButton[] thirdRow = new CardButton[5];
        private List<CardButton> currentHand = new List<CardButton>();

        //private List<CardButton> currentHand = new List<CardButton>(10);
        private Label[] firstRowLabels = new Label[7];
        private Label[] secondRowLabels = new Label[5];
        private Label[] thirdRowLabels = new Label[5];
        public bool gameOver;

        public GraphicsBoard()
        {
            InitializeComponent();
            //drawCorrectImage(exitButton);
            board = DominionCards.GameBoard.getInstance();
            board.Subscribe(this);
            Console.WriteLine("\nTHE NUMBER OF PLAYERS IN THIS GAME IS: " + board.turnOrder.Count + "\n");
            SetUpImagesDictionary();

            imageadd = new System.Drawing.Bitmap[] { DominionGUI.Properties.Resources.WorkshopHalf, DominionGUI.Properties.Resources.AdventurerHalfNew, DominionGUI.Properties.Resources.BureaucratHalf, DominionGUI.Properties.Resources.CellarHalf, DominionGUI.Properties.Resources.ChancellorHalf, DominionGUI.Properties.Resources.ChapelHalf, DominionGUI.Properties.Resources.CouncilroomHalf, DominionGUI.Properties.Resources.FeastHalf, DominionGUI.Properties.Resources.FestivalHalf, DominionGUI.Properties.Resources.GardensHalf, DominionGUI.Properties.Resources.LaboratoryHalf, DominionGUI.Properties.Resources.LibraryHalf, DominionGUI.Properties.Resources.MarketHalf, DominionGUI.Properties.Resources.MilitiaHalf, DominionGUI.Properties.Resources.MineHalf, DominionGUI.Properties.Resources.MoatHalf, DominionGUI.Properties.Resources.MoneylenderHalf, DominionGUI.Properties.Resources.RemodelHalf, DominionGUI.Properties.Resources.SmithyHalf, DominionGUI.Properties.Resources.SpyHalf, DominionGUI.Properties.Resources.ThiefHalf, DominionGUI.Properties.Resources.ThroneroomHalf, DominionGUI.Properties.Resources.VillageHalf, DominionGUI.Properties.Resources.WitchHalf, DominionGUI.Properties.Resources.WoodcutterHalf };
            cardsadd = new System.Type[] { typeof(DominionCards.KingdomCards.Workshop),typeof(DominionCards.KingdomCards.Adventurer),typeof(DominionCards.KingdomCards.Bureaucrat),typeof(DominionCards.KingdomCards.Cellar),typeof(DominionCards.KingdomCards.Chancellor),typeof(DominionCards.KingdomCards.Chapel),typeof(DominionCards.KingdomCards.CouncilRoom),typeof(DominionCards.KingdomCards.Feast),typeof(DominionCards.KingdomCards.Festival),typeof(DominionCards.KingdomCards.Gardens),typeof(DominionCards.KingdomCards.Laboratory),typeof(DominionCards.KingdomCards.Library),typeof(DominionCards.KingdomCards.Market),typeof(DominionCards.KingdomCards.Militia),typeof(DominionCards.KingdomCards.Mine),typeof(DominionCards.KingdomCards.Moat),typeof(DominionCards.KingdomCards.MoneyLender),typeof(DominionCards.KingdomCards.Remodel),typeof(DominionCards.KingdomCards.Smithy),typeof(DominionCards.KingdomCards.Spy),typeof(DominionCards.KingdomCards.Thief),typeof(DominionCards.KingdomCards.ThroneRoom),typeof(DominionCards.KingdomCards.Village),typeof(DominionCards.KingdomCards.Witch),typeof(DominionCards.KingdomCards.Woodcutter)};
            basiccard = new System.Type[] { typeof(DominionCards.KingdomCards.Gold), typeof(DominionCards.KingdomCards.Silver), typeof(DominionCards.KingdomCards.Copper) ,typeof(DominionCards.KingdomCards.Province),typeof(DominionCards.KingdomCards.Duchy),typeof(DominionCards.KingdomCards.Estate),typeof(DominionCards.KingdomCards.Curse)};
            //addRandomtencards();
            SetBuyableCards();
            DrawBuyableCards();
            UpdateLabelsAndHand();
            this.Update();
            this.Show();

            try
            {
                Thread thread1 = new Thread(board.GameRunner);
                thread1.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("The game is over");
                EndGame();
            }
         
        }

        public static void WaitToUpdateLabels()
        {
            lock (DominionCards.GameBoard.UpdateGraphicsLock)
            {
                Monitor.Wait(DominionCards.GameBoard.UpdateGraphicsLock);
                GraphicsBoard.getinstance().UpdateLabelsAndHand();
                Monitor.PulseAll(DominionCards.GameBoard.UpdateGraphicsLock);
            }
        }

        private void WaitToUpdate()
        {
            while(!board.GameIsOver()){
                lock (DominionCards.GameBoard.UpdateGraphicsLock)
                {
                    Monitor.Wait(DominionCards.GameBoard.UpdateGraphicsLock);
                    UpdateLabelsAndHand();
                    Monitor.PulseAll(DominionCards.GameBoard.UpdateGraphicsLock);
                }
                Thread.Sleep(500);
            }
        }

        private void SetUpImagesDictionary()
        {
            cardImages = new Dictionary<DominionCards.Card, System.Drawing.Bitmap>();
            cardImages.Add(new DominionCards.KingdomCards.Gold(), DominionGUI.Properties.Resources.GoldHalf);
            cardImages.Add(new DominionCards.KingdomCards.Silver(), DominionGUI.Properties.Resources.SilverHalf);
            cardImages.Add(new DominionCards.KingdomCards.Copper(), DominionGUI.Properties.Resources.CopperHalf);
            cardImages.Add(new DominionCards.KingdomCards.Province(), DominionGUI.Properties.Resources.ProvinceHalf);
            cardImages.Add(new DominionCards.KingdomCards.Duchy(), DominionGUI.Properties.Resources.DuchyHalf);
            cardImages.Add(new DominionCards.KingdomCards.Estate(), DominionGUI.Properties.Resources.EstateHalf);
            cardImages.Add(new DominionCards.KingdomCards.Curse(), DominionGUI.Properties.Resources.CurseHalf);
            cardImages.Add(new DominionCards.KingdomCards.Adventurer(), DominionGUI.Properties.Resources.AdventurerHalfNew);
            cardImages.Add(new DominionCards.KingdomCards.Bureaucrat(), DominionGUI.Properties.Resources.BureaucratHalf);
            cardImages.Add(new DominionCards.KingdomCards.Cellar(), DominionGUI.Properties.Resources.CellarHalf);
            cardImages.Add(new DominionCards.KingdomCards.Chancellor(), DominionGUI.Properties.Resources.ChancellorHalf);
            cardImages.Add(new DominionCards.KingdomCards.Chapel(), DominionGUI.Properties.Resources.ChapelHalf);
            cardImages.Add(new DominionCards.KingdomCards.CouncilRoom(), DominionGUI.Properties.Resources.CouncilroomHalf);
            cardImages.Add(new DominionCards.KingdomCards.Feast(), DominionGUI.Properties.Resources.FeastHalf);
            cardImages.Add(new DominionCards.KingdomCards.Festival(), DominionGUI.Properties.Resources.FestivalHalf);
            cardImages.Add(new DominionCards.KingdomCards.Gardens(), DominionGUI.Properties.Resources.GardensHalf);
            cardImages.Add(new DominionCards.KingdomCards.Laboratory(), DominionGUI.Properties.Resources.LaboratoryHalf);
            cardImages.Add(new DominionCards.KingdomCards.Library(), DominionGUI.Properties.Resources.LibraryHalf);
            cardImages.Add(new DominionCards.KingdomCards.Market(), DominionGUI.Properties.Resources.MarketHalf);
            cardImages.Add(new DominionCards.KingdomCards.Militia(), DominionGUI.Properties.Resources.MilitiaHalf);
            cardImages.Add(new DominionCards.KingdomCards.Mine(), DominionGUI.Properties.Resources.MineHalf);
            cardImages.Add(new DominionCards.KingdomCards.Moat(), DominionGUI.Properties.Resources.MoatHalf);
            cardImages.Add(new DominionCards.KingdomCards.MoneyLender(), DominionGUI.Properties.Resources.MoneylenderHalf);
            cardImages.Add(new DominionCards.KingdomCards.Remodel(), DominionGUI.Properties.Resources.RemodelHalf);
            cardImages.Add(new DominionCards.KingdomCards.Smithy(), DominionGUI.Properties.Resources.SmithyHalf);
            cardImages.Add(new DominionCards.KingdomCards.Spy(), DominionGUI.Properties.Resources.SpyHalf);
            cardImages.Add(new DominionCards.KingdomCards.Thief(), DominionGUI.Properties.Resources.ThiefHalf);
            cardImages.Add(new DominionCards.KingdomCards.ThroneRoom(), DominionGUI.Properties.Resources.ThroneroomHalf);
            cardImages.Add(new DominionCards.KingdomCards.Village(), DominionGUI.Properties.Resources.VillageHalf);
            cardImages.Add(new DominionCards.KingdomCards.Witch(), DominionGUI.Properties.Resources.WitchHalf);
            cardImages.Add(new DominionCards.KingdomCards.Woodcutter(), DominionGUI.Properties.Resources.WoodcutterHalf);
            cardImages.Add(new DominionCards.KingdomCards.Workshop(), DominionGUI.Properties.Resources.WorkshopHalf);
        }

        private void DrawBuyableCards(){
            int xValue = 50;
            int yValue = 100;
            DrawingHelper(firstRow, firstRowLabels, xValue, yValue);
            xValue = 220;
            yValue = 300;
            DrawingHelper(secondRow, secondRowLabels, xValue, yValue);
            xValue = 220;
            yValue = 500;
            DrawingHelper(thirdRow, thirdRowLabels, xValue, yValue);
        }
        private void DrawingHelper(CardButton[] buttons, Label[] labels, int xValue, int yValue)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Height = 155;
                buttons[i].Width = 200;
                buttons[i].Location = new Point(xValue, yValue);
                buttons[i].ForSale = true;

                buttons[i].BackgroundImage = new System.Drawing.Bitmap(cardImages[buttons[i].card]);
                buttons[i].BackgroundImageLayout = ImageLayout.Stretch;
                Controls.Add(buttons[i]);
                buttons[i].Parent = this;
                buttons[i].InitializeEventHandler();

                labels[i].Location = new Point(xValue + 50, yValue + 155);
                Controls.Add(labels[i]);
                labels[i].Parent = this;
                xValue += 256;
            }
        }
        private void HandDrawingHelper()
        {
            int xValue = 220;
            int yValue = 725;
            int startingX = xValue;
            int numberColumns = 6;
            int xIncriment = 220;
            for (int i = 0; i < currentHand.Count; i++)
            {
                currentHand[i].Height = 155;
                currentHand[i].Width = 200;
                currentHand[i].Location = new Point(xValue, yValue);

                currentHand[i].BackgroundImage = new System.Drawing.Bitmap(cardImages[currentHand[i].card]);
                currentHand[i].BackgroundImageLayout = ImageLayout.Stretch;
                currentHand[i].Parent = this;
                currentHand[i].InitializeEventHandler();
                Controls.Add(currentHand[i]);

               //currentHand.Add(buttons[i]);

                xValue += xIncriment;
                if (xValue > startingX + (xIncriment * (numberColumns-1)))
                {
                    xValue = startingX;
                    yValue += 165;
                }
            }
        }

        public void addbasiccards()
        {
            CardButton Gold = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[0]));
            Gold.BackgroundImage = new System.Drawing.Bitmap(DominionGUI.Properties.Resources.GoldHalf);
            Gold.Click += new EventHandler(this.gameplay);
            Gold.BackgroundImageLayout = ImageLayout.Stretch;
            Gold.Height = 155;
            Gold.Width = 200;
            CardButton Silver = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[1]));
            CardButton Copper = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[2]));
            CardButton Province = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[3]));
            CardButton Duchy = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[4]));
            CardButton Estate = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[5]));
            CardButton Curse = new CardButton((DominionCards.Card)Activator.CreateInstance(basiccard[6]));
            
        }

        /*public void addRandomtencards()
        {
            List<int> numList = new List<int>();
            numList = RandomGenerateCards.GenerateRandom.GenerateRandomList(25, 10);
            int xValue = 220;
            int yValue = 300;
            for (int i = 0; i < 25; i++)
            {
                if (numList.Contains(i))
                {
                    CardButton newButton = new CardButton((DominionCards.Card)Activator.CreateInstance(cardsadd[i]));
                    newButton.BackgroundImage = imageadd[i];
                    board.addCard((DominionCards.Card)Activator.CreateInstance(cardsadd[i]));
                    newButton.Click += new EventHandler(this.gameplay);
                    newButton.BackgroundImageLayout = ImageLayout.Stretch;
                    newButton.Height = 155;
                    newButton.Width = 200;

                    if (xValue > 1300)
                    {
                        xValue = 220;
                        yValue = 500;
                    }
                    newButton.Location = new Point(xValue, yValue);
                    xValue = xValue + 256;
                    Controls.Add(newButton);
                    newButton.Parent = this;
                    this.Update();
                    this.Show();
                }
            }
        }*/
        public void DrawHand()
        {
            SetCurrentPlayerHand();
            HandDrawingHelper();
        }
        private void gameplay(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Visible = true;
        }

        public static GraphicsBoard getinstance()
        {
            if (instance == null)
                instance = new GraphicsBoard();
            return instance;
        }


        /**
         * private helper, returns an array of buttons that should be drawn to the Form.
         */
        private void SetCurrentPlayerHand()
        {
            DominionCards.Player current = DominionCards.GameBoard.getInstance().turnOrder.Peek();
            List<Card> playerHand = current.getHand();
            currentHand = new List<CardButton>(10);
            Console.WriteLine();
            for (int i = 0; i < playerHand.Count; i++)
            {
                CardButton butt = new CardButton((DominionCards.Card)playerHand[i]);
                currentHand.Add(butt);
                Console.WriteLine(butt + " was addded!");
            }
            Console.WriteLine();
        }

        /**
         * get all buyable cards
         */
        private void SetBuyableCards()
        {
            int count = 0;
            foreach (DominionCards.Card card in DominionCards.GameBoard.getInstance().GetCards().Keys)
            {
                Dictionary<DominionCards.Card, int> dict = DominionCards.GameBoard.getInstance().GetCards();
                if (count < 7)
                {
                    int index = count;
                    firstRow[index] = new CardButton(card);
                    Label newLabel = new Label();
                    newLabel.Text = "Cards Left: " + dict[card];
                    firstRowLabels[index] = newLabel;
                }
                else if (count < 12)
                {
                    int index = count - 7;
                    secondRow[index] = new CardButton(card);
                    Label newLabel = new Label();
                    newLabel.Text = "Cards Left: " + dict[card];
                    secondRowLabels[index] = newLabel;
                }
                else
                {
                    int index = count - 12;
                    thirdRow[index] = new CardButton(card);
                    Label newLabel = new Label();
                    newLabel.Text = "Cards Left: " + dict[card];
                    thirdRowLabels[index] = newLabel;
                }
                count++;
            }
        }

        public void UpdateLabelsAndHand()
        {
            UpdateCardLabelsHelper(firstRow, firstRowLabels);
            UpdateCardLabelsHelper(secondRow, secondRowLabels);
            UpdateCardLabelsHelper(thirdRow, thirdRowLabels);
            UpdateMiscLabels();
            UpdateHand();
            this.Update();
        }
        private void UpdateHand()
        {
            int i;
            Console.WriteLine();
            for (i = 0; i < currentHand.Count; i++)
            {
                Console.WriteLine(currentHand[i] + " was removed!");
                this.Controls.Remove(currentHand[i]);
            }
            Console.WriteLine();
            DrawHand();
        }

        private void UpdateMiscLabels()
        {
            DominionCards.Player current = board.turnOrder.Peek();
            this.actionleft.Text = "Actions: " + current.actionsLeft();
            this.buyleft.Text = "Buys: " + current.buysLeft();
            this.moneyleft.Text = "Money: " + current.moneyLeft();
            this.decksize.Text = "Deck Size: " + current.getDeck().Count;
            this.discardsize.Text = "Discard Size: " + current.getDiscard().Count;
            this.playerLabel.Text = "It is player " + current.getNumber() + "'s turn. -- "
                + GetGamePhaseText() + " -- Round " + GameBoard.getInstance().GetTurnNumber();
        }
        private string GetGamePhaseText()
        {
            if (DominionCards.GameBoard.getGamePhase() == 0 || DominionCards.GameBoard.getGamePhase() == 1)
            {
                return "Action Phase";
            }
            else if (DominionCards.GameBoard.getGamePhase() == 2 || DominionCards.GameBoard.getGamePhase() == 3)
            {
                return "Buy Phase";
            }
            else 
            {
                throw new Exception("Game entered an invalid state");
            }

        }

        private void UpdateCardLabelsHelper(CardButton[] cardButtons, Label[] labels)
        {
            Dictionary<DominionCards.Card, int> dict = DominionCards.GameBoard.getInstance().GetCards();
            for (int i = 0; i < cardButtons.Length; i++)
            {
                int cardsLeft = dict[cardButtons[i].card];
                labels[i].Text = "Cards Left: " + cardsLeft;
            }
        }
        private void MainBoard_Load(object sender, EventArgs e)
        {

        }
        private void SkipPhase(object sender, EventArgs e)
        {
            //Action phase
            if (DominionCards.GameBoard.getGamePhase() == 1)
            {
                lock (DominionCards.GameBoard.ActionPhaseLock)
                {   
                    DominionCards.GameBoard.AbortPhase = true;
                    Monitor.PulseAll(DominionCards.GameBoard.ActionPhaseLock);
                }
            }
            //Buy phase
            else if (DominionCards.GameBoard.getGamePhase() == 3)
            {
                lock (DominionCards.GameBoard.BuyPhaseLock)
                {
                    DominionCards.GameBoard.AbortPhase = true;
                    Monitor.PulseAll(DominionCards.GameBoard.BuyPhaseLock);
                }
            }
            Thread.Sleep(500);
            lock (DominionCards.GameBoard.UpdateGraphicsLock)
                {
                    //DominionCards.GameBoard.AbortPhase = true;
                    Monitor.PulseAll(DominionCards.GameBoard.UpdateGraphicsLock);
                }

            UpdateLabelsAndHand(); //Buy Phase Currently
        }
        private void exitButtonClick(object sender, EventArgs e)
        {
            EndGame();
        }
        private void EndGame()
        {
            DominionCards.GameBoard.AbortGame = true;
            //Action Phase
            if (DominionCards.GameBoard.getGamePhase() == 1)
            {
                lock (DominionCards.GameBoard.ActionPhaseLock)
                {
                    Monitor.PulseAll(DominionCards.GameBoard.ActionPhaseLock);
                }
            }
            //Buy Phase
            if (DominionCards.GameBoard.getGamePhase() == 3)
            {
                lock (DominionCards.GameBoard.BuyPhaseLock)
                {
                    Monitor.PulseAll(DominionCards.GameBoard.BuyPhaseLock);
                }
            }

            Thread.Sleep(250);
            lock (DominionCards.GameBoard.UpdateGraphicsLock)
            {
                Monitor.PulseAll(DominionCards.GameBoard.UpdateGraphicsLock);
            }

            Close();
            SelectNumPlayers.getInstance().Dispose();
        }
        // observer code


        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(GameBoard value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { UpdateLabelsAndHand(); }));
            }
            else
            {
                UpdateLabelsAndHand();
            }
        }
    }
}
