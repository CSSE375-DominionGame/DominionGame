
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

        public override List<Card> SelectCards(Decision decision, List<Card> choices)
        {
            SelectCardsForm form = new SelectCardsForm(choices, decision.getText(), decision.getMaxCards());
            form.TopMost = true;
            form.GetSelection(); // this must mutate choices
            return choices;
        }
        public override List<Card> SelectCards(Decision decision)
        {
            // TODO get rid of dis shiz
            List<Card> choices = decision.getCardSelection(this);
            SelectCardsForm form = new SelectCardsForm(choices, decision.getText(), decision.getMaxCards());
            form.GetSelection(); // this must mutate choices
            return choices;
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


        /*public override List<Card> SelectCards(List<Card> cards, String name, int numCards)
        {
            List<Card> copyCards = cards;
            SelectCardsForm form = new SelectCardsForm(copyCards, name, numCards);
            form.GetSelection(); // mutates ArrayList cards
            Console.WriteLine("Player finished selecting cards.");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine("Card ID " + ((Card)cards[i]).getID() + " selected");
            }
            return copyCards;
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
        public override void actionPhase()
        {
            lock (GameBoard.ActionPhaseLock)
            {
                GameBoard.lastCardPlayed = null;
                Console.WriteLine("PLAYER: Action Phase called on player " + getNumber());
                GameBoard.gamePhase = actionPhaseInt;
                Monitor.Wait(GameBoard.ActionPhaseLock);
                GameBoard.gamePhase = buyPhaseInt;
                Console.WriteLine("PLAYER: Button pulse recieved.");
                Card cardPlayed = GameBoard.lastCardPlayed;
                try
                {
                    if (cardPlayed == null)
                    {
                        Console.WriteLine("Action Phase terminated.");
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
                Console.WriteLine("PLAYER: finished playing card, pulse sent.");
                Console.WriteLine("Playing a card with ID " + cardPlayed.getID());
            }
        }
        public override void buyPhase()
        {
            GameBoard.lastCardBought = null;
            Console.WriteLine("Buy Phase called on player " + getNumber());
            lock (GameBoard.BuyPhaseLock)
            {
                Console.WriteLine("PLAYER: Action Phase called on player " + getNumber());
                GameBoard.gamePhase = endPhaseInt;
                Monitor.Wait(GameBoard.BuyPhaseLock);
                GameBoard.gamePhase = limboPhaseInt;
                Console.WriteLine("PLAYER: Button pulse recieved.");
                Card cardBought = GameBoard.lastCardBought;
                try
                {
                    if (cardBought == null)
                    {
                        Console.WriteLine("Buy Phase terminated.");
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
                
                Console.WriteLine("PLAYER: finished playing card, pulse sent.");
                Console.WriteLine("Buying a card with ID " + cardBought.getID());
            }

        }
    }
}
