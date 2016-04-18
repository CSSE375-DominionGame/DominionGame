using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{

    class ChapelDecision : DefaultDecision
    {
        private static readonly string MSG = "select up to 4 cards to trash.";
        private static readonly string ERR_MSG = "you selected too many cards.";

        public ChapelDecision()
            : base(0, 4, MSG, ERR_MSG, false) { }

        public override List<Card> getCardSelection(Player player)
        {
            return DefaultDecision.copyList(player.getHand());
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            List<Card> hand = player.getHand();
            foreach (Card c in cardsSelected)
            {
                TrashOneCard(hand, c);
            }
        }

        private void TrashOneCard(List<Card> hand, Card toTrash)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].getID() == toTrash.getID())
                {
                    hand.RemoveAt(i);
                    return;
                }
            }
        }

    }
}
