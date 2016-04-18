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
            player.MakeDecision(decision);
        }

        public override bool IsAction()
        {
            return true;
        }
    }
}
