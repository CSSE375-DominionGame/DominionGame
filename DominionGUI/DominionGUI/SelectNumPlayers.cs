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
        private int numPlayers = 2;
        private int numAI = 0;

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
            if (checkBox1.Checked && lastChecked != checkBox1)
            {
                UncheckOtherButtons(checkBox1);
                lastChecked = checkBox1;
                numPlayers = 2;
                numAI = 0;
            }
            else if (checkBox2.Checked && lastChecked != checkBox2)
            {
                UncheckOtherButtons(checkBox2);
                lastChecked = checkBox2;
                numPlayers = 3;
                numAI = 0;
            }
            else if (checkBox3.Checked && lastChecked != checkBox3)
            {
                UncheckOtherButtons(checkBox3);
                lastChecked = checkBox3;
                numPlayers = 4;
                numAI = 0;
            }
            else if (checkBox4.Checked && lastChecked != checkBox4)
            {
                UncheckOtherButtons(checkBox4);
                lastChecked = checkBox4;
                numPlayers = 1;
                numAI = 1;
            }
            else if (checkBox5.Checked && lastChecked != checkBox5)
            {
                UncheckOtherButtons(checkBox5);
                lastChecked = checkBox5;
                numPlayers = 1;
                numAI = 2;
            }
            else if (checkBox6.Checked && lastChecked != checkBox6)
            {
                UncheckOtherButtons(checkBox6);
                lastChecked = checkBox6;
                numPlayers = 1;
                numAI = 3;
            }
            //if (lastChecked != null)
            /*else
            {
                UncheckOtherButtons(null);
                /*if (lastChecked.Equals(checkBox1) && !checkBox1.Checked)
                {
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                }/
            }*/
        }

        private void UncheckOtherButtons(CheckBox excluded)
        {
            CheckBox[] toUncheck = new CheckBox[] { checkBox1, checkBox2, checkBox3,
                checkBox4, checkBox5, checkBox6 };
            foreach (CheckBox button in toUncheck)
            {
                if (button != excluded)
                {
                    button.Checked = false;
                }
            }
            Refresh();
        }

        public DominionCards.GameBoard getboard()
        {
            return this.board;
        }

        private void RunGame(object sender, EventArgs e)
        {
            var myForm = new FormSupplySetup(this.numPlayers, this.numAI);
            myForm.Update();
            myForm.Show();
            this.Hide();
        }
    }
}


