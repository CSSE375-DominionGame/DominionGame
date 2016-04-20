using DominionCards.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards
{
    public class ActionCard : Card
    {
        public int cards, money, buys, actions;
        protected IDecision decision;
        public ActionCard(string cardName, int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(price, idNumb)
        {
            cards = extraCards;
            money = extraMoney;
            buys = extraBuys;
            actions = extraActions;
            decision = new NullDecision();
        }
        public override int getVictoryPoints()
        {
            return 0;
        }
        public override void Play(Player player)
        {
            List<Card> cardsSelected = player.MakeDecision(decision);
            while (!this.decision.cardSelectionValid(cardsSelected))
            {
                displaySelectionError();
                cardsSelected = player.MakeDecision(decision);
            }
            this.decision.applyDecisionTo(player, cardsSelected);
        }

        protected virtual void displaySelectionError()
        {
            string msg;
            msg = getSelectionErrorMessage();
            MessageBox.Show(msg);
        }

        public string getSelectionErrorMessage()
        {
            string msg;
            if (this.decision.getMaxCards() == this.decision.getMinCards())
            {
                msg = String.Format("You must select {0} cards. Try again",
                    this.decision.getMaxCards());
            }
            else
            {
                msg = String.Format("You must select between {0} and {1} cards. Try again",
                    this.decision.getMinCards(), this.decision.getMaxCards());
            }
            return msg;
        }

        public override bool IsAction()
        {
            return true;
        }
    }
}
