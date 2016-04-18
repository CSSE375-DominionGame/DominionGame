using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    public class ChancelorDecision : YesNoDecision
    {
        private int Count = -1;
        public ChancelorDecision()
            : base("would you like to discard your deck?") {}

        public override bool cardSelectionValid(List<Card> cards)
        {
            return true;
        }

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> selection = new List<Card>();
            foreach (Card c in player.getDeck())
            {
                selection.Add(c);
            }
            return selection;
        }
        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            foreach (Card c in cardsSelected)
            {
                player.getDiscard().Add(c);
            }
            player.getDeck().Clear();
        }

        public override List<Card> getYesCards(Player player)
        {
            List<Card> cards = new List<Card>();
            foreach (Card c in player.getDeck())
            {
                cards.Add(c);
            }
            return cards;
        }
    }
}
