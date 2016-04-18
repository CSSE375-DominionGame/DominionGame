using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    class NullDecision : Decision
    {
        public NullDecision()
            : base(0, 0, "") { }

        /*public override List<Card> MakeDecision(Player player) {
            return new List<Card>();
        }*/

        public override bool cardSelectionValid(List<Card> cards)
        {
            return true;
        }

        public override List<Card> getCardSelection(Player player)
        {
            return new List<Card>();
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            // do nothing
        }
    }
}
