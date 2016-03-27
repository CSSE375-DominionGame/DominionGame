using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class ActionCard : Card
    {
        public int cards, money, buys, actions;
        public ActionCard(string cardName, int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(price, idNumb)
        {
            cards = extraCards;
            money = extraMoney;
            buys = extraBuys;
            actions = extraActions;
        }
        public override int getVictoryPoints()
        {
            return 0;
        }
        public override void Play(Player player)
        {
            Console.WriteLine("I am just an action card!!");
        }
        public override bool IsAction()
        {
            return true;
        }
    }
}
