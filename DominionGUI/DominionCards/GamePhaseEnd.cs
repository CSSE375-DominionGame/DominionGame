using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class GamePhaseEnd
    {
        private Player currentPlayer;
        private Stack<Card> deck;
        private List<Card> discard;
        private List<Card> hand;

        public void setEndPhase(Player player)
        {
            this.currentPlayer = player;
            this.deck = player.getDeck();
            this.discard = player.getDiscard();
            this.hand = player.getHand();
        }

        public int countVictoryPoints()
        {
            int vps = 0;
            int numGardens = 0;
            double deckCount = 0;
            Stack<Card> tempStack = new Stack<Card>();
            // get points from cards in deck
            while (deck.Count > 0)
            {
                Card card = deck.Pop();
                deckCount++;
                if (card.getID() == 14)
                {
                    numGardens++;
                }
                vps += card.getVictoryPoints();
                tempStack.Push(card);
            }
            // Returns all cards to the deck
            while (tempStack.Count > 0)
            {
                deck.Push(tempStack.Pop());
            }
            // get points for cards in discard
            for (int i = 0; i < discard.Count; i++)
            {
                vps += ((Card)discard[i]).getVictoryPoints();
            }
            // get points for cards in hand
            for (int i = 0; i < hand.Count; i++)
            {
                vps += ((Card)hand[i]).getVictoryPoints();
            }
            deckCount = Math.Floor(deckCount);
            return (vps + (numGardens * ((int)deckCount) / 10));
        }

        public int getTotalMoney()
        {
            int money = countTreasureInDeck();
            money += countTreasureInDiscard();
            money += currentPlayer.moneyInHand();
            return money;
        }

        private int countTreasureInDiscard()
        {
            int money = 0;
            for (int i = 0; i < discard.Count; i++)
            {
                Card card = (Card)discard[i];
                if (card.IsTreasure())
                {
                    money += ((TreasureCard)card).getValue();
                }
            }
            return money;
        }

        private int countTreasureInDeck()
        {
            int money = 0;
            Stack<Card> tempStack = new Stack<Card>();
            while (deck.Count > 0)
            {
                Card card = deck.Pop();
                tempStack.Push(card);
                if (card.IsTreasure())
                {
                    money += ((TreasureCard)card).getValue();
                }
            }
            pushStackToDeck(tempStack);
            return money;
        }

        private void pushStackToDeck(Stack<Card> stack)
        {
            while (stack.Count > 0)
            {
                deck.Push(stack.Pop());
            }
        }
    }
}
