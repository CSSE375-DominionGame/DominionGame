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

        public List<Card> SelectByGraphic(Player player)
        {
            List<Card> choices = getCardSelection(player);
            SelectCardsForm form = new SelectCardsForm(choices, this.text, this.max);
            form.GetSelection(); // this must mutate choices
            return choices;
        }
        public virtual List<Card> MakeDecision(Player player)
        {
            return player.MakeDecision(this);
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