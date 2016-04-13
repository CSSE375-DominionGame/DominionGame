using DominionCards.KingdomCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.Decisions
{
    class MineDecision : Decision
    {
        public MineDecision() : base(0, 1, "Select a Treasure Card to Upgrade") { }

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> newCards = new List<Card>();

            foreach (Card c in player.getHand())
            {
                if (c.IsTreasure())
                {
                    newCards.Add(c);
                }
            }
            return newCards;
        }

        public override void applyDecisionTo(Player player, List<Card> cards)
        {
            
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
