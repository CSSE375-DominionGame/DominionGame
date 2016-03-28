using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Mine : ActionCard
    {
        private static int ID = 19;
        public Mine()
            : base("Mine", 0, 0, 0, 0, 5, ID)
        {
            // Uses ActionCard Constructor
        }

        public override String ToString()
        {
            return "Mine";
        }
        public override void Play(Player player)
        {

            List<Card> treasureCards = new List<Card>();
            foreach (Card card in player.getHand())
            {
                int id = card.getID();
                if (id == 0 || id == 1)
                {
                    treasureCards.Add(card);
                }
            }
            if (treasureCards.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the mine!");
                return;
            }
            //List<Card> cards = player.SelectCards(treasureCards, "Choose a card to upgrade.", 1);
            List<Card> cards = player.SelectCards(this.decision);
            while (cards.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to upgrade.  Try again");
                //cards = player.SelectCards(treasureCards, "Choose a card to upgrade.", 1);
                cards = player.SelectCards(this.decision);
            }
            Card cardSelected = (Card)cards[0];

            if (cardSelected.getID() == 0)
            {
                if (GameBoard.getInstance().getCardsLeft(new Silver()) == 0)
                {
                    MessageBox.Show("There are no silver cards left");
                    return;
                }
                player.addCardToHand(new Silver());
                player.getHand().Remove(cardSelected);
                GameBoard.getInstance().GetCards()[new Silver()] -= 1;
            }
            else if (cardSelected.getID() == 1)
            {
                if (GameBoard.getInstance().getCardsLeft(new Gold()) == 0)
                {
                    MessageBox.Show("There are no gold cards left");
                    return;
                }
                player.addCardToHand(new Gold());
                player.getHand().Remove(cardSelected);
                GameBoard.getInstance().GetCards()[new Gold()] -= 1;
            }
        }
    }
}