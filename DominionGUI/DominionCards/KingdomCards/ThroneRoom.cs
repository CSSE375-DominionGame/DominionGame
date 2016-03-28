using System;
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
            : base("Throne Room", 0, 0, 0, 0, 4, ID)
        {
            // Uses ActionCard Constructor
        }

        public override void Play(Player player)
        {
            List<Card> actionCards = new List<Card>();
            foreach (Card card in player.getHand())
            {
                int id = card.getID();
                if (id > 5 && id != 14)
                {
                    actionCards.Add(card);
                }
            }
            if (actionCards.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the throne room!");
                return;
            }
            // List<Card> cards = player.SelectCards(actionCards, "Choose a card to play twice.", 1);
            List<Card> cards = player.SelectCards(this.decision);
            while (cards.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to play twice.  Try again");
                //cards = player.SelectCards(actionCards, "Choose a card to play twice.", 1);
                cards = player.SelectCards(this.decision);
            }
            Card cardPlayed = (Card)cards[0];

            player.actions += 2;
            player.playCard(cardPlayed);
            player.addCardToHand(cardPlayed);
            player.getDiscard().Remove(cardPlayed);
            player.playCard(cardPlayed);
        }
        public override String ToString()
        {
            return "Throne Room";
        }
    }
}
