using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    class FeastDecision : Decision
    {
        private static readonly string MSG = "select a card to gain.";
        private static readonly string ERR_MSG = "???";

        public FeastDecision() 
            : base(1, 1, MSG, ERR_MSG, false) { }

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> list = new List<Card>();
            Dictionary<Card, int> dict = GameBoard.getInstance().cards;
            foreach (Card c in dict.Keys)
            {
                if (dict[c] > 0) {
                    list.Add(c);
                }
            }
            return list;
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            player.getDiscard().RemoveAt(player.getDiscard().Count - 1);
            player.getDiscard().Add(cardsSelected[0]);
            GameBoard.getInstance().cards[cardsSelected[0]] -= 1;
        }
    }
}
