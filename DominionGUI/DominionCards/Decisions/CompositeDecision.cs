using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    class CompositeDecision : IDecision
    {
        private IDecision baseDecision;
        public CompositeDecision(IDecision decision)
        {
            baseDecision = decision;
        }

        public int getMaxCards()
        {
            return baseDecision.getMaxCards();
        }

        public int getMinCards()
        {
            return baseDecision.getMinCards();
        }

        public List<Card> SelectByGraphic(Player player)
        {
            return baseDecision.SelectByGraphic(player);
        }

        public string getText()
        {
            return baseDecision.getText();
        }

        public bool isCancelable()
        {
            return baseDecision.isCancelable();
        }

        public bool cardSelectionValid(List<Card> cards)
        {
            return baseDecision.cardSelectionValid(cards);
        }

        public void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            baseDecision.applyDecisionTo(player, cardsSelected);
        }
    }
}
