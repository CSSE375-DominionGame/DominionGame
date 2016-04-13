using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public abstract class Decision : IDecision
    {
        private int max, min;
        private Boolean cancelable;
        private String text, errorMsg;

        public Decision(int min, int max, string text)
            : this(min, max, text, "bad selection - try again", false) {}
        
        public Decision(int min, int max, string text, string errorMsg, bool cancel)
        {
            this.max = max;
            this.min = min;
            this.cancelable = cancel;
            this.text = text;
            this.errorMsg = errorMsg;
        }

        public int getMaxCards()
        {
            return max;
        }

        public int getMinCards()
        {
            return min;
        }

        public string getText()
        {
            return text;
        }

        public string getErrorMsg()
        {
            return errorMsg;
        }

        public Boolean isCancelable()
        {
            return cancelable;
        }

        public virtual bool cardSelectionValid(List<Card> cards)
        {
            if (cards.Count == 0 && cancelable)
            {
                return true;
            }
            if (cards.Count < min || cards.Count > max)
            {
                return false;
            }
            return true;
        }

        /**
         * This method should return the list of cards that the player may select from. The player will
         * select which cards the card's effect will apply to, and the selected cards will be used in the
         * card
         */
        public abstract List<Card> getCardSelection(Player player);
        public abstract void applyDecisionTo(Player player, List<Card> cardsSelected);
        public virtual List<Card> SelectCards(Player player)
        {
            return player.SelectCards(this);
        }

        protected static List<Card> copyList(List<Card> list)
        {
            List<Card> copy = new List<Card>();
            foreach (Card c in list)
            {
                copy.Add(c);
            }
            return copy;
        }
    }
}