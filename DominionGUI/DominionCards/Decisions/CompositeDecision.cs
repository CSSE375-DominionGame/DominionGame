using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.Decisions;

namespace DominionCards.Decisions
{
    public class CompositeDecision : IDecision
    {
        private CompositeDecision next;
        private IDecision leaf;
        public CompositeDecision(IDecision decision, CompositeDecision next)
        {
            this.leaf = decision;
            this.next = next;
        }
        public CompositeDecision(IDecision decision)
        {
            this.leaf = decision;
            this.next = null;
        }

        public int getMaxCards()
        {
            // return leaf.getMaxCards();
            throw new NotImplementedException("CompositeDecision.getMax(...)");
        }

        public int getMinCards()
        {
            // return leaf.getMinCards();
            throw new NotImplementedException("CompositeDecision.getMin(...)");
        }

        public List<Card> SelectByGraphic(Player player)
        {
            return CompositeSelect(player, getCardSelection(player));
        }

        protected virtual List<Card> CompositeSelect(Player player, List<Card> cards) {
            if (cards == null)
            {
                cards = getCardSelection(player);
            }
            cards = player.MakeDecision(leaf);
            if (next != null)
            {
                cards = next.CompositeSelect(player, cards);
            }
            return cards;
        }

        public string getText()
        {
            // return leaf.getText();
            throw new NotImplementedException("CompositeDecision.getText(...)");
        }

        public bool isCancelable()
        {
            // return leaf.isCancelable();
            throw new NotImplementedException("CompositeDecision.isCancelable(...)");
        }

        public bool cardSelectionValid(List<Card> cards)
        {
            // return leaf.cardSelectionValid(cards);
            throw new NotImplementedException("CompositeDecision.cardSelectionValid(...)");
        }

        public void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            // leaf.applyDecisionTo(player, cardsSelected);
            throw new NotImplementedException("CompositeDecision.applyDecisionTo(...)");
        }

        public List<Card> getCardSelection(Player player)
        {
            throw new NotImplementedException("This decision is not intended to be made first.");
        }
    }
}
