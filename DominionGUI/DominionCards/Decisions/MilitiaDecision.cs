using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.Decisions
{
    public class MilitiaDecision : DefaultDecision
    {
        public MilitiaDecision()
            : base(3, 3, "Select 3 cards to Keep!") {} // do nothing

        public override List<Card> getCardSelection(Player player)
        {
            List<Card> newList = new List<Card>();
            foreach (Card c in player.getHand())
            {
                newList.Add(c); // copy hand to avoid mutation.
            }
            return newList;
        }

        public override void applyDecisionTo(Player player, List<Card> cards)
        {
            Dictionary<Card, int> cardsToKeep = countCards(cards);
            Dictionary<Card, int> cardsFound = new Dictionary<Card, int>();
            Stack<int> indeciesRemoved = new Stack<int>();
            for (int i = 0; i < player.getHand().Count; i++)
            {
                Card c = player.getHand()[i];
                if (! cardsToKeep.ContainsKey(c)) 
                {
                    indeciesRemoved.Push(i);
                }
                else if (! cardsFound.ContainsKey(c)) 
                {
                    cardsFound[c] = 1;
                }
                else if (cardsFound[c] == cardsToKeep[c])
                {
                    indeciesRemoved.Push(i);
                }
                else
                {
                    cardsFound[c] += 1;
                }
            }

            while (indeciesRemoved.Count > 0)
            {
                int i = indeciesRemoved.Pop();
                Card c = player.getHand()[i];
                player.getDiscard().Add(c);
                player.getHand().RemoveAt(i);
            }
        }


        private Dictionary<Card, int> countCards(List<Card> cards)
        {
            Dictionary<Card, int> cardCount = new Dictionary<Card, int>();
            foreach (Card c in cards)
            {
                if (cardCount.ContainsKey(c))
                {
                    cardCount[c] += 1;
                }
                else
                {
                    cardCount[c] = 1;
                }
            }
            return cardCount;
        }
    }
}
