using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    public abstract class LeafDecision : DefaultDecision
    {
        protected List<Card> cardInput = new List<Card>();
        public LeafDecision(int min, int max, string text)
            : base(min, max, text) {}

        public void setCardInput(List<Card> cardInput)
        {
            this.cardInput = cardInput;
        }

        public override List<Card> getCardSelection(Player player)
        {
            return cardInput;
        }
    }
}
