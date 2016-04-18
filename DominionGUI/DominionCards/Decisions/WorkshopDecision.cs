using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    class WorkshopDecision : DefaultDecision
    {
        public WorkshopDecision()
            : base(1, 1, "select a card to gain from the workshop!") {}

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> buyableCards = new List<Card>();
            foreach (Card card in GameBoard.getInstance().cards.Keys)
            {
                int cost = card.getPrice();
                if (cost < 5)
                {
                    buyableCards.Add(card);
                }
            }
            return buyableCards;
        }
        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            if (cardsSelected.Count == 1) {
                player.getDiscard().Add(cardsSelected[0]);
                GameBoard.getInstance().GetCards()[cardsSelected[0]] -= 1;
            }
            else if (cardsSelected.Count > 1) {
                Console.WriteLine("too many cards from workshop! TODO - add exception");
            }
            else
            {
                Console.WriteLine("nothing was made with workshop!");
            }
        }

    }
}
