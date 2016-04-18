﻿
﻿using System;
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
        public override void MakeDecision(IDecision decision)
        {
            List<Card> cardsSelected = decision.SelectByGraphic(this);
            while (!decision.cardSelectionValid(cardsSelected))
            {
                displaySelectionError(decision);
                cardsSelected = decision.SelectByGraphic(this);
            }
            decision.applyDecisionTo(this, cardsSelected);
        }
        private void displaySelectionError(IDecision decision)
        {
            string msg;
            if (decision.getMaxCards() == decision.getMinCards())
            {
                if (decision.getMaxCards() == 1)
                {
                    msg = String.Format("You must select 1 card. Try again");
                }
                else
                {
                    msg = String.Format("You must select {0} cards. Try again",
                        decision.getMaxCards());
                }
            }
            else
            {
                msg = String.Format("You must select between {0} and {1} cards. Try again",
                    decision.getMinCards(), decision.getMaxCards());
            }
            MessageBox.Show(msg);
        }

        private static List<Card> copyList(List<Card> list)
        {
            List<Card> copy = new List<Card>();
            foreach (Card c in list)
            {
                copy.Add(c);
            }
            return copy;
        }
        public override void actionPhase()
        {
            lock (GameBoard.ActionPhaseLock)
            {
                GameBoard.lastCardPlayed = null;
                GameBoard.gamePhase = actionPhaseInt;
                Monitor.Wait(GameBoard.ActionPhaseLock);
                GameBoard.gamePhase = buyPhaseInt;
                Card cardPlayed = GameBoard.lastCardPlayed;
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
            GameBoard.lastCardBought = null;
            lock (GameBoard.BuyPhaseLock)
            {
                GameBoard.gamePhase = endPhaseInt;
                Monitor.Wait(GameBoard.BuyPhaseLock);
                GameBoard.gamePhase = limboPhaseInt;
                Card cardBought = GameBoard.lastCardBought;
                try
                {
                    if (cardBought == null)
                    {
                        Monitor.PulseAll(GameBoard.BuyPhaseLock);
                        return;
                    }
                    if (!buyCard(cardBought))
                    {
                        if (GameBoard.getInstance().getCardsLeft(cardBought) == 0)
                        {
                            MessageBox.Show("There are none left to buy!");
                        }
                        else if (moneyLeft() < cardBought.getPrice())
                        {
                            MessageBox.Show("You do not have enough money to buy that!");
                        }
                        else
                        {
                            MessageBox.Show("You can't buy that!");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                
                }

        }
    }
}
