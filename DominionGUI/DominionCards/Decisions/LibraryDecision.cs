using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.Decisions
{
    public class LibraryDecision : YesNoDecision
    {

        private Card card;
        public LibraryDecision() : base("")
        {}

        public override List<Card> SelectByGraphic(Player player)
        {
            this.card = player.GetNextCard();
            DialogResult result1 = MessageBox.Show(getDecisionMessage(card), "yes/no?", MessageBoxButtons.YesNo);
            bool yesSelected = "Yes".Equals(result1.ToString());
            if (yesSelected)
            {
                return getYesCards(player);
            }
            else
            {
                return getNoCards(player);
            }
        }

        private string getDecisionMessage(Card c)
        {
            return "You drew a " + c.GetName() + ", if it is an Action Card you may chose to discard it.";
        }


        private void next(Player player)
        {
            if (player.getHand().Count < 7)
            {
                applyDecisionTo(player, player.MakeDecision(this));
            }
        }

        public override bool cardSelectionValid(List<Card> cards)
        {
            return true;
        }

        protected override void doNo(Player player, List<Card> cardsSelected)
        {
            player.getHand().Add(this.card);
            next(player);
        }

        protected override void doYes(Player player, List<Card> cardsSelected)
        {
            if (this.card.IsAction())
            {
                player.getDiscard().Add(this.card);
                next(player);
            }
            else
            {
                MessageBox.Show("You cannot discard that, it is not an action card");
                doNo(player, cardsSelected);
            }
            
        }
    }
}
