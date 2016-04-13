using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    class CellarDecision : Decision
    {
        public CellarDecision() : base(0, Int32.MaxValue, "select cards to discard with a Cellar") { }

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> handCopy = new List<Card>();
            foreach (Card c in player.getHand())
            {
                handCopy.Add(c);
            }
            return handCopy;
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            for (int i = 0; i < cardsSelected.Count; i++)
            {
                player.getHand().Add(player.GetNextCard());
                Card cardSelected = (Card)cardsSelected[i];
                player.getHand().Remove(cardSelected);
                player.getDiscard().Add(cardSelected);
            }
        }

    }
}
