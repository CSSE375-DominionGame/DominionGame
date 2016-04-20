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

        protected override void doYes(Player player, List<Card> cardsSelected)
        {
            while (player.getDeck().Count > 0)
            {
                player.getDiscard().Add(player.getDeck().Pop());
            }
        }
    }
}
