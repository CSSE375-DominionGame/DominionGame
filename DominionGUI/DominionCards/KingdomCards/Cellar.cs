using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Cellar : ActionCard
    {
        public Cellar()
            : base("Cellar", 0, 0, 0, 1, 2, 8)
        {
            // Uses ActionCard Constructor
        }

        public override String ToString()
        {
            return "Cellar";
        }

        /*public override void Play(Player player)
        {
            List<Card> discardableCards = new List<Card>();
            foreach (Card card in player.getHand())
            {
                discardableCards.Add(card);

            }
            if (discardableCards.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the cellar");
                return;
            }
            //List<Card> cards = player.SelectCards(discardableCards, "Choose cards to trash.", player.getHand().Count);
            List<Card> cards = player.SelectCards(this.decision);
            //This doesn't need a check to make sure you didn't check too many boxes because you can check all of them
            for (int i = 0; i < cards.Count; i++)
            {
                player.getHand().Add(player.GetNextCard());
                Card cardSelected = (Card)cards[i];
                player.getHand().Remove(cardSelected);
                player.getDiscard().Add(cardSelected);
            }
        }*/
    }
}
