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
        private LeafDecision leaf;
        public CompositeDecision(LeafDecision decision, CompositeDecision next)
        {
            this.leaf = decision;
            this.next = next;
        }
        public CompositeDecision(LeafDecision decision)
        {
            this.leaf = decision;
            this.next = null;
        }

        public int getMaxCards()
        {
            return leaf.getMaxCards();
        }

        public int getMinCards()
        {
            return leaf.getMinCards();
        }

        public List<Card> SelectByGraphic(Player player)
        {
            return CompositeSelect(player, getCardSelection(player));
        }
        protected void setCardInput(List<Card> cardInput)
        {
            leaf.setCardInput(cardInput);
        }

        protected virtual List<Card> CompositeSelect(Player player, List<Card> cards) {
            if (cards == null)
            {
                cards = getCardSelection(player);
            }
            cards = player.MakeDecision(leaf);
            leaf.applyDecisionTo(player, cards);
            if (next != null)
            {
                next.setCardInput(cards);
                cards = player.MakeDecision(next);
            }
            return cards;
        }

        public string getText()
        {
            return leaf.getText();
        }

        public bool isCancelable()
        {
            return leaf.isCancelable();
        }

        public bool cardSelectionValid(List<Card> cards)
        {
            return leaf.cardSelectionValid(cards);
        }

        public void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            // do nothing
            // leaf.applyDecisionTo(player, cardsSelected);
        }

        public List<Card> getCardSelection(Player player)
        {
            return leaf.getCardSelection(player);
        }
    }
}
