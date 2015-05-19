﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Remodel : ActionCard
    {
        private static int ID = 22;
        public Remodel()
            : base(0, 0, 0, 0, 4, ID)
        {
            // TODO implement
        }

        public override void Play(Player player)
        {

            //pick a card to trash
            ArrayList trashableCards = new ArrayList();
            foreach (Card card in player.getHand())
            {
                trashableCards.Add(card);
                
            }
            if (trashableCards.Count == 0)
            {
                MessageBox.Show("You have no cards to trash with the remodel!");
                return;
            }
            ArrayList cards = player.SelectCards(trashableCards, "Choose a card to trash", 1);
            while (cards.Count > 1)
            {
                DialogResult result1 = MessageBox.Show("You may only select 1 card to trash.  Try again");
                cards = player.SelectCards(trashableCards, "Choose a card to trash.", 1);
            }
            Card cardSelected = (Card)cards[0];
            int costOfCardSelected = cardSelected.getPrice();
            player.getHand().Remove(cardSelected);

            //pick a card to gain
            ArrayList buyableCards = new ArrayList();
            foreach (Card card in GameBoard.getInstance().cards.Keys)
            {
                int cost = card.getPrice();
                if (cost < 3 + costOfCardSelected)
                {
                    buyableCards.Add(card);
                }
            }
            if (buyableCards.Count == 0)
            {
                MessageBox.Show("You have no cards to buy with the remodel!");
                return;
            }
            ArrayList cards2 = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
            while (cards2.Count > 1)
            {
                DialogResult result1 = MessageBox.Show("You may only select 1 card to gain.  Try again");
                cards2 = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
            }
            Card cardSelected2 = (Card)cards2[0];
            player.getDiscard().Add(cardSelected2);
            GameBoard.getInstance().GetCards()[cardSelected2] -= 1;


        }
        public override String ToString()
        {
            return "Remodel";
        }
    }
}
