using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    class ThroneRoomDecision : DefaultDecision
    {
        private static readonly string MSG = "";
        private static readonly string ERR_MSG = "";
        public ThroneRoomDecision()
            : base(0, 1, MSG, ERR_MSG, false) { }

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> actionCards = new List<Card>();
            foreach (Card card in player.getHand())
            {
                if (card.IsAction())
                {
                    actionCards.Add(card);
                }
            }
            return actionCards;
            // List<Card> cards = player.SelectCards(actionCards, "Choose a card to play twice.", 1);
            
        }

        public override void applyDecisionTo(Player player, List<Card> cardsSelected)
        {
            if (cardsSelected.Count == 0)
            {
                // TODO print an error message.
                return;
            }
            Card cardPlayed = cardsSelected[0];
            player.actions += 2;
            player.playCard(cardPlayed);
            player.addCardToHand(cardPlayed);
            player.getDiscard().Remove(cardPlayed);
            player.playCard(cardPlayed);
            /*if (cardsSelected.Count == 0)
            {
                return;
            }
            cardsSelected[0].Play(player);
            player.getDiscard().RemoveAt(player.getDiscard().Count - 1);
            cardsSelected[0].Play(player);*/
        }
    }
}
