﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards;
using DominionCards.KingdomCards;

namespace DominionGUI
{
    public partial class FormSupplySetup : Form
    {
        private List<Card> cards = new List<Card>() {
                new Adventurer(), new Bureaucrat(), new Cellar(), new Chancellor(), new Chapel(), 
                new CouncilRoom(), new Feast(), new Festival(), new Gardens(), 
                new Laboratory(), new Library(), new Market(), new Militia(), 
                new Mine(), new Moat(), new MoneyLender(), new Remodel(), 
                new Smithy(), new Spy(), new Thief(), new ThroneRoom(), 
                new Village(), new Witch(), new Woodcutter(), new Workshop()};

        private List<Card> supply = new List<Card>();
        private int numPlayers;

        public FormSupplySetup(int numPlayers)
        {
            InitializeComponent();
            this.numPlayers = numPlayers;
            foreach (Card c in cards)
            {
                ckls_cards.Items.Add(c.ToString());
            }

            this.btn_ok.Enabled = true;
            ckls_cards.CheckOnClick = true;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            List<int> uncheckedIndices = new List<int>();
            for (int i = 0; i < ckls_cards.Items.Count; i++)
            {
                if (!ckls_cards.CheckedItems.Contains(ckls_cards.Items[i])) {
                    uncheckedIndices.Add(i);
                }
            }
            Console.WriteLine(ckls_cards.CheckedItems.Count);
            Random r = new Random();
            for (int i = ckls_cards.CheckedItems.Count; i < 10; i++)
            {
                Console.WriteLine(i);
                int ck = r.Next(uncheckedIndices.Count);
                ckls_cards.SetItemChecked(uncheckedIndices[ck], true);
                uncheckedIndices.RemoveAt(ck);
            }
            Console.WriteLine(ckls_cards.CheckedItems.Count);
            foreach (int i in ckls_cards.CheckedIndices)
            {
                supply.Add(cards[i]);
            }
            Console.WriteLine(supply.Count);
            Dictionary<Card, int> gameCards = makeCards();
            GameBoard board = new GameBoard(gameCards);
            createplayers(this.numPlayers, board);
            var myform = GraphicsBoard.getinstance();
            GraphicsBoard.WaitToUpdateLabels();
            myform.Update();
            myform.Show();
            this.Hide();
        }

        public Dictionary<Card, int> makeCards()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            int numberOfVictoryCards;
            int numberOfCurses;
            if (this.numPlayers == 2)
            {
                numberOfVictoryCards = 8;
                numberOfCurses = 10;
            }
            else if (this.numPlayers == 3)
            {
                numberOfVictoryCards = 12;
                numberOfCurses = 20;
            }
            else if (numPlayers == 4)
            {
                numberOfVictoryCards = 12;
                numberOfCurses = 30;
            }
            else
            {
                throw new Exception("Number of Players must be 2, 3 or 4");
            }

            cards.Add(new DominionCards.KingdomCards.Copper(), 60);
            cards.Add(new DominionCards.KingdomCards.Silver(), 40);
            cards.Add(new DominionCards.KingdomCards.Gold(), 30);
            cards.Add(new DominionCards.KingdomCards.Estate(), numberOfVictoryCards);
            cards.Add(new DominionCards.KingdomCards.Duchy(), numberOfVictoryCards);
            cards.Add(new DominionCards.KingdomCards.Province(), numberOfVictoryCards);
            cards.Add(new DominionCards.KingdomCards.Curse(), numberOfCurses);

            for (int i = 0; i < 10; i++)
            {
                cards.Add(this.supply[i],10);
            }

            return cards;
        }

        public void createplayers(int numberplayer, GameBoard board)
        {
            for (int i = 0; i < numberplayer; i++)
            {
                board.AddPlayer(new DominionCards.HumanPlayer(i + 1));
            }
        }

        private void ckls_cards_SelectedIndexChanged(object sender, EventArgs e) { }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void ckls_cards_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkCount = ckls_cards.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked)
            {
                checkCount++;
            }
            else
            {
                checkCount--;
            }

            if (checkCount <= 10)
            {
                btn_ok.Enabled = true;
            }
            else
            {
                btn_ok.Enabled = false;
            }
        }

        private void btn_randomize_Click(object sender, EventArgs e)
        {
            foreach (int i in ckls_cards.CheckedIndices)
            {
                ckls_cards.SetItemChecked(i, false);
            }
            List<int> uncheckedIndices = new List<int>();
            for (int i = 0; i < ckls_cards.Items.Count; i++)
            {
                uncheckedIndices.Add(i);
            }
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int ck = r.Next(uncheckedIndices.Count);
                ckls_cards.SetItemChecked(uncheckedIndices[ck], true);
                uncheckedIndices.RemoveAt(ck);
            }
            /*
            FormGame game = new FormGame();
            game.setPlayerCount(numPlayers);
            game.Show();
            */
        }
    }
}