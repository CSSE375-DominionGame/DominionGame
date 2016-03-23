using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    abstract class Decision
    {
        private int max, min;
        private Boolean cancel;
        private String text, errorMsg;

        public Decision(int min, int max, String text)
            : this(min, max, text, "bad selection - try again", false) {}
        
        public Decision(int min, int max, String text, String errorMsg, Boolean cancel)
        {
            this.max = max;
            this.min = min;
            this.cancel = cancel;
            this.text = text;
            this.errorMsg = errorMsg;
        }
        /**
         * This method should return the list of cards that the player may select from. The player will
         * select which cards the card's effect will apply to, and the selected cards will be used in the
         * card
         */
        public abstract List<Card> getCardSelection();
        public abstract void applyDecisionTo(List<Card> cardsSelected);
    }
}