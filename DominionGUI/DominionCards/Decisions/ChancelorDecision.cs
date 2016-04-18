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

        /*public override List<Card> getCardSelection(Player player)
        {
            
        }*/
        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            if (cardsSelected.Count != 0)
            {
                while (player.getDeck().Count > 0)
                {
                    player.getDiscard().Add(player.getDeck().Pop());
                }
            }
        }

        public override List<Card> getYesCards(Player player)
        {
            List<Card> list = new List<Card>();
            list.Add(null);
            return list;
        }
    }
}
