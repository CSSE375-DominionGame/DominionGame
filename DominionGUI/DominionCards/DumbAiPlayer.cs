using DominionCards.Decisions;
using DominionCards.KingdomCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DominionCards
{
    public class DumbAiPlayer : Player
    {
        public DumbAiPlayer(int playerNumber)
            : base()
        {
            setNumber(1);
        }

        public override void actionPhase()
        {
            while (IsActionPhase())
            {
                Card toPlay = selectCardToBuy();
                playCard(toPlay);
            }
        }

        protected virtual Card selectCardToPlay()
        {
            foreach (Card c in getHand())
            {
                if (c.IsAction())
                {
                    return c;
                }
            }
            throw new Exception("no playable cards in AI's hand");
        }

        public override void buyPhase()
        {
            while (IsBuyPhase())
            {
                Card c = selectCardToBuy();
                this.buyCard(c);
                Thread.Sleep(500); // Give player time to see what AI is doing
            }
        }

        protected virtual Card selectCardToBuy()
        {
            int buyingPrice = this.moneyLeft();
            while (buyingPrice > 0)
            {
                Card c = findCardToBuyAt(buyingPrice--);
                if (c != null)
                {
                    return c;
                }
            }
            return new Copper();
        }

        protected virtual Card findCardToBuyAt(int price)
        {
            // TODO make this smarter
            foreach (Card c in GameBoard.getInstance().cards.Keys)
            {
                if (c.getPrice() == price)
                {
                    return c;
                }
            }
            return null;
        }

        public override List<Card> MakeDecision(IDecision decision)
        {
            if (decision is NullDecision)
            {
                return new List<Card>();
            }
            else if (decision is YesNoDecision)
            {
                return DecideYesNo((YesNoDecision)decision);
            }
            else
            {
                return DecideDefault((DefaultDecision)decision);
            }

            // throw new NotImplementedException();
        }

        protected virtual List<Card> DecideYesNo(YesNoDecision decision)
        {
            List<Card> list = new List<Card>();
            list.Add(null); // always decide yes
            return list;
        }
        protected virtual List<Card> DecideDefault(DefaultDecision decision)
        {
            int i = decision.getMaxCards();
            List<Card> options = decision.getCardSelection(this);
            List<Card> selection = new List<Card>();
            foreach (Card c in options)
            {
                if ((i--) <= 0) { break; }
                selection.Add(c);
            }
            return selection;
        }
    }
}
