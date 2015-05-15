﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class ThroneRoom : ActionCard
    {
        private static int ID = 26;
        public ThroneRoom()
            : base(0, 0, 0, 0, 4, ID)
        {
            // TODO implement
        }

        public override void Play(Player player)
        {
            ArrayList actionCards = new ArrayList();
            foreach (Card card in player.getHand()){
                int id = card.getID();
                if (id > 5 && id != 14){
                    actionCards.Add(card);
                }
            }
            if (actionCards.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the throne room!");
                return;
            }
            ArrayList cards = player.SelectCards(actionCards, "Choose a card to play twice.", 1);
            Card cardPlayed = (Card) cards[0];

            player.actions += 2;
            player.playCard(cardPlayed);
            player.addCardToHand(cardPlayed);
            player.getDiscard().Remove(cardPlayed);
            player.playCard(cardPlayed);
        }
    }
}
