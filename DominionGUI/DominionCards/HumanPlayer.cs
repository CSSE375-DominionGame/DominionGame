using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards
{
    public class HumanPlayer : Player
    {
        public static readonly int limboPhaseInt = 0;
        public static readonly int actionPhaseInt = 1;
        public static readonly int buyPhaseInt = 2;
        public static readonly int endPhaseInt = 3;

        /*public override List<Card> SelectCards(IDecision decision, List<Card> choices)
        {
            SelectCardsForm form = new SelectCardsForm(choices, decision.getText(), decision.getMaxCards());
            form.GetSelection(); // this must mutate choices
            return choices;
        }*/
        public HumanPlayer()
            : base()
        {
            setNumber(1);
            // TODO implement
        }
        public HumanPlayer(int playerNumber)
            : base()
        {
            setNumber(playerNumber);
        }
        public override List<Card> MakeDecision(IDecision decision)
        {
            return decision.SelectByGraphic(this);
        }
        public override void actionPhase()
        {
            lock (GameBoard.ActionPhaseLock)
            {
                GameBoard.setLastCardPlayed(null);
                GameBoard.setGamePhase(actionPhaseInt);
                Monitor.Wait(GameBoard.ActionPhaseLock);
                GameBoard.setGamePhase(buyPhaseInt);
                Card cardPlayed = GameBoard.getLastCardPlayed();
                try
                {
                    if (cardPlayed == null)
                    {
                        Monitor.PulseAll(GameBoard.ActionPhaseLock);
                        return;
                    }
                    playCard(cardPlayed);
                }
                catch (CardCannotBePlayedException e)
                {
                    MessageBox.Show(e.Message);
                }
                Monitor.PulseAll(GameBoard.ActionPhaseLock);
            }
        }
        public override void buyPhase()
        {
            GameBoard.setLastCardBought(null);
            lock (GameBoard.BuyPhaseLock)
            {
                GameBoard.setGamePhase(endPhaseInt);
                Monitor.Wait(GameBoard.BuyPhaseLock);
                GameBoard.setGamePhase(limboPhaseInt);
                Card cardBought = GameBoard.getLastCardBought();
                try
                {
                    if (cardBought == null)
                    {
                        Monitor.PulseAll(GameBoard.BuyPhaseLock);
                        return;
                    }
                    if (!buyCard(cardBought))
                    {
                        MessageBox.Show(cardBuyingFailureMessage(cardBought));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                Monitor.PulseAll(GameBoard.BuyPhaseLock);

            }

        }

        public string cardBuyingFailureMessage(Card cardBought)
        {
            string errMsg;
            if (GameBoard.getInstance().getCardsLeft(cardBought) == 0)
            {
                errMsg = "There are none left to buy!";
            }
            else if (moneyLeft() < cardBought.getPrice())
            {
                errMsg = "You do not have enough money to buy that!";
            }
            else
            {
                errMsg = "You can't buy that!";
            }
            return errMsg;
        }
    }
}
