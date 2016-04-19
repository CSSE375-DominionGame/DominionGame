using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    public class RemodelTrashDecision : LeafDecision
    {
        public RemodelTrashDecision() 
            : base(0, 1, "select a card to Trash and upgrade") {}

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> list = new List<Card>();
            foreach (Card c in player.getHand())
            {
                list.Add(c);
            }
            return list;
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            if (cardsSelected.Count != 1) { throw new Exception("selection went bad..."); }
            for (int i = 0; i < player.getHand().Count; i++) {
                if (player.getHand()[i].Equals(cardsSelected[0])) 
                {
                    player.getHand().RemoveAt(i);
                    return;
                }
            }
        }
    }
}
