using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards.Decisions;

namespace DominionCards.Decisions
{
    class RemodelDecision : Decision
    {

        public RemodelDecision() : base(0, 1, "Remodel a Card") { }

        public override List<Card> getCardSelection(Player player)
        {
            return Decision.copyList(player.getHand());
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {

            Card trashCardSelected = (Card)cardsSelected[0];
            int costOftrashCardSelected = trashCardSelected.getPrice();

            //if (trashCardSelected.Equals(null))
            //{
                //MessageBox.Show("You have no cards to trash with the remodel!");
                //return;
            //}

            

            List<Card> buyableCards = new List<Card>();

            foreach (Card card in GameBoard.getInstance().cards.Keys)
            {
                int cost = card.getPrice();
                if (cost < 10 + costOftrashCardSelected)
                {
                    buyableCards.Add(card);
                }
            }

            if (buyableCards.Count == 0)
            {
                MessageBox.Show("You have no cards to buy with the remodel!");
                return;
            }

            List<Card> upgradedCards = player.SelectCards(new NullDecision(), buyableCards);
            Card upgradedCard = (Card)upgradedCards[0];

            player.getDiscard().Add(upgradedCard);
            player.getHand().Remove(trashCardSelected);
            GameBoard.getInstance().GetCards()[upgradedCard] -= 1;
        }
    }
}
