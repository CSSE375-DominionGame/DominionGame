using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.Decisions
{
    public abstract class YesNoDecision : IDecision
    {
        private string text;
        public YesNoDecision(string text) 
        {
            this.text = text;
        }
        public int getMinCards()
        {
            return 0;
        }
        public int getMaxCards()
        {
            return Int32.MaxValue;
        }

        public string getText()
        {
            return text;
        }
        public string getErrorMsg()
        {
            return "YES-NO-DECISION SHOULD NEVER ERROR";
        }
        public Boolean isCancelable()
        {
            return false;
        }
        public List<Card> SelectByGraphic(Player player)
        {
            DialogResult result1 = MessageBox.Show("You played a chancellor. Would you like to discard your hand now?",
                "You played a Chancellor",
                MessageBoxButtons.YesNo);
            bool yesSelected = "yes".Equals(result1.ToString());
            if (yesSelected){
                return getYesCards(player);
            }
            else
            {
                return getNoCards(player);
            }
        }

        public abstract List<Card> getYesCards(Player player);
        public virtual List<Card> getNoCards(Player player)
        {
            return new List<Card>();
        }

        public abstract bool cardSelectionValid(List<Card> cards);
        //public abstract List<Card> getCardSelection(Player player);
        public abstract void applyDecisionTo(Player player, List<Card> cardsSelected);
        /*public List<Card> MakeDecision(Player player)
        {
            return player.MakeDecision(this);
        }*/

    }
}
