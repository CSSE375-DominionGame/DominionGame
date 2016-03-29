using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    abstract class YesNoDecision : Decision
    {
        public YesNoDecision(string text) 
            : base(1, 1, text, "", false) {}
        public override List<Card> SelectCards(Player player)
        {
            return new List<Card>();
        }

        public override bool cardSelectionValid(List<Card> cards)
        {
            return true;
        }

        public override List<Card> getCardSelection(Player player)
        {
            return new List<Card>();
        }
    }
}
