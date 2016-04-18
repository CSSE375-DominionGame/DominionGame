using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.KingdomCards;

namespace DominionCards.Decisions
{
    class MoneyLenderDecision : YesNoDecision
    {
        public MoneyLenderDecision()
            : base("Discard a Copper to gain +3 money") {}

        public override bool cardSelectionValid(List<Card> cards)
        {
            foreach (Card c in cards)
            {
                if (c.IsTreasure() && ((TreasureCard)c).getValue() == 1)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void doYes(Player player, List<Card> cardsSelected)
        {
            int moneyToAdd = 3;
            Card cardSelected = (Card)cardsSelected[0];

            player.addMoneyToHand(moneyToAdd);
            player.getHand().Remove(cardSelected);
            GameBoard.getInstance().GetCards()[new Copper()] -= 1;
        }

        protected override void doNo(Player player, List<Card> cardsSelected)
        {
            base.doNo(player, cardsSelected);
        }
    }
}
