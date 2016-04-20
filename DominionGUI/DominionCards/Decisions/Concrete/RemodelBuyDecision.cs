using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    public class RemodelBuyDecision : LeafDecision
    {
        public RemodelBuyDecision()
            : base(0, 1, "select a card to gain from remodel") {}

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> buyableCards = new List<Card>();
            int priceCap = cardInput[0].getPrice() + 2;
            foreach (Card c in GameBoard.getInstance().GetCards().Keys)
            {
                if (GameBoard.getInstance().getCardsLeft(c) > 0
                    && c.getPrice() <= priceCap)
                {
                    buyableCards.Add(c);
                }
            }
            return buyableCards;
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            Card c = cardsSelected[0];
            player.getDiscard().Add(c);
            GameBoard.getInstance().GetCards()[c] -= 1;
        }
    }
}
