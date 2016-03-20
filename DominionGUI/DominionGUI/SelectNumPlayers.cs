using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionGUI
{
    public partial class SelectNumPlayers : Form
    {
        private static System.Drawing.Bitmap[] imageadd = new System.Drawing.Bitmap[] { DominionGUI.Properties.Resources.WorkshopHalf, DominionGUI.Properties.Resources.AdventurerHalfNew, DominionGUI.Properties.Resources.BureaucratHalf, DominionGUI.Properties.Resources.CellarHalf, DominionGUI.Properties.Resources.ChancellorHalf, DominionGUI.Properties.Resources.ChapelHalf, DominionGUI.Properties.Resources.CouncilroomHalf, DominionGUI.Properties.Resources.FeastHalf, DominionGUI.Properties.Resources.FestivalHalf, DominionGUI.Properties.Resources.GardensHalf, DominionGUI.Properties.Resources.LaboratoryHalf, DominionGUI.Properties.Resources.LibraryHalf, DominionGUI.Properties.Resources.MarketHalf, DominionGUI.Properties.Resources.MilitiaHalf, DominionGUI.Properties.Resources.MineHalf, DominionGUI.Properties.Resources.MoatHalf, DominionGUI.Properties.Resources.MoneylenderHalf, DominionGUI.Properties.Resources.RemodelHalf, DominionGUI.Properties.Resources.SmithyHalf, DominionGUI.Properties.Resources.SpyHalf, DominionGUI.Properties.Resources.ThiefHalf, DominionGUI.Properties.Resources.ThroneroomHalf, DominionGUI.Properties.Resources.VillageHalf, DominionGUI.Properties.Resources.WitchHalf, DominionGUI.Properties.Resources.WoodcutterHalf };
        private static System.Type[] cardsadd = new System.Type[] { typeof(DominionCards.KingdomCards.Workshop), typeof(DominionCards.KingdomCards.Adventurer), typeof(DominionCards.KingdomCards.Bureaucrat), typeof(DominionCards.KingdomCards.Cellar), typeof(DominionCards.KingdomCards.Chancellor), typeof(DominionCards.KingdomCards.Chapel), typeof(DominionCards.KingdomCards.CouncilRoom), typeof(DominionCards.KingdomCards.Feast), typeof(DominionCards.KingdomCards.Festival), typeof(DominionCards.KingdomCards.Gardens), typeof(DominionCards.KingdomCards.Laboratory), typeof(DominionCards.KingdomCards.Library), typeof(DominionCards.KingdomCards.Market), typeof(DominionCards.KingdomCards.Militia), typeof(DominionCards.KingdomCards.Mine), typeof(DominionCards.KingdomCards.Moat), typeof(DominionCards.KingdomCards.MoneyLender), typeof(DominionCards.KingdomCards.Remodel), typeof(DominionCards.KingdomCards.Smithy), typeof(DominionCards.KingdomCards.Spy), typeof(DominionCards.KingdomCards.Thief), typeof(DominionCards.KingdomCards.ThroneRoom), typeof(DominionCards.KingdomCards.Village), typeof(DominionCards.KingdomCards.Witch), typeof(DominionCards.KingdomCards.Woodcutter) };
        private static System.Type[] basiccard = new System.Type[] { typeof(DominionCards.KingdomCards.Gold), typeof(DominionCards.KingdomCards.Silver), typeof(DominionCards.KingdomCards.Copper), typeof(DominionCards.KingdomCards.Province), typeof(DominionCards.KingdomCards.Duchy), typeof(DominionCards.KingdomCards.Estate), typeof(DominionCards.KingdomCards.Curse) };
        public DominionCards.GameBoard board;
        public static SelectNumPlayers INSTANCE = null;
        Label discarddeck = new Label();
        CheckBox lastChecked;
        private int numberplayers = 2;

        public static SelectNumPlayers getInstance()
        {
            if (INSTANCE == null)
            {
                // sets this to INSTANCE in constructor
                new SelectNumPlayers();
            }
            return INSTANCE;
        }

        public SelectNumPlayers()
        {
            INSTANCE = this;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void SelectNumPlayers_Load(object sender, EventArgs e)
        {

        }

        private void Playeroption(object sender, EventArgs e)
        {


            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                lastChecked = checkBox1;
                numberplayers = 2;
            }
            else if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                lastChecked = checkBox2;
                numberplayers = 3;
            }
            else if (checkBox3.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                lastChecked = checkBox3;
                numberplayers = 4;
            }
            if (!lastChecked.Equals(null))
                if (lastChecked.Equals(checkBox1) && !checkBox1.Checked)
                {
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                }
            if (lastChecked.Equals(checkBox2) && !checkBox2.Checked)
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
            }
            if (lastChecked.Equals(checkBox3) && !checkBox3.Checked)
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
        }
        public void createplayers(int numberplayer)
        {
            for (int i = 0; i < numberplayer; i++)
            {
                this.board.AddPlayer(new DominionCards.HumanPlayer(i + 1));
            }
        }

        public DominionCards.GameBoard getboard()
        {
            return this.board;
        }
        /* private void button1_Click(object sender, EventArgs e)
        {
            // do nothing?
        } */
        private void RunGame(object sender, EventArgs e)
        {
            var myForm = new FormSupplySetup(this.numberplayers);
            myForm.Update();
            myForm.Show();
            this.Hide();

            //board = new dominioncards.gameboard(createrandomcarddictionary());
            //createplayers(numberplayers);
            //var myform = graphicsboard.getinstance();
            //graphicsboard.waittoupdatelabels();
            //myform.update();
            //myform.show();
        }
        private Dictionary<DominionCards.Card, int> CreateRandomCardDictionary()
        {
            int numberOfVictoryCards;
            int numberOfCurses;
            if (numberplayers == 2)
            {
                numberOfVictoryCards = 8;
                numberOfCurses = 10;
            }
            else if (numberplayers == 3)
            {
                numberOfVictoryCards = 12;
                numberOfCurses = 20;
            }
            else if (numberplayers == 4)
            {
                numberOfVictoryCards = 12;
                numberOfCurses = 30;
            }
            else
            {
                numberOfVictoryCards = 999;
                numberOfCurses = 999;
            }

            Dictionary<DominionCards.Card, int> dict = new Dictionary<DominionCards.Card, int>();
            List<int> numList = new List<int>();
            dict.Add(new DominionCards.KingdomCards.Copper(), 60);
            dict.Add(new DominionCards.KingdomCards.Silver(), 40);
            dict.Add(new DominionCards.KingdomCards.Gold(), 30);
            dict.Add(new DominionCards.KingdomCards.Estate(), numberOfVictoryCards);
            dict.Add(new DominionCards.KingdomCards.Duchy(), numberOfVictoryCards);
            dict.Add(new DominionCards.KingdomCards.Province(), numberOfVictoryCards);
            dict.Add(new DominionCards.KingdomCards.Curse(), numberOfCurses);

            numList = RandomGenerateCards.GenerateRandom.GenerateRandomList(25, 10);
            for (int i = 0; i < 25; i++)
            {
                if (numList.Contains(i))
                {
                    int numCards = 10;
                    DominionCards.Card card = (DominionCards.Card)Activator.CreateInstance(cardsadd[i]);
                    if (card.Equals(new DominionCards.KingdomCards.Gardens()))
                    {
                        numCards = numberOfVictoryCards;
                    }

                    dict.Add(card, numCards);
                }
            }
            return dict;
        }
    }


}


