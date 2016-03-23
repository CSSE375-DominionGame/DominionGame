
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
        /* public ArrayList makeDecision(Decision decision)
        {
            ArrayList selection = (ArrayList) decision.getCardSelection(this).Clone();
            SelectCardsForm form = new SelectCardsForm(selection, decision.getText(), decision.getMaxCards());
            form.GetSelection(); // ???
            return selection;
        } */

        public override ArrayList SelectCards(ArrayList cards, String name, int numCards)
        {
            ArrayList copyCards = (ArrayList)cards.Clone();
            SelectCardsForm form = new SelectCardsForm(copyCards, name, numCards);
            form.GetSelection(); // mutates ArrayList cards
            Console.WriteLine("Player finished selecting cards.");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine("Card ID " + ((Card)cards[i]).getID() + " selected");
            }
            return copyCards;
        }

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
                GameBoard.gamePhase = 1;
                Monitor.Wait(GameBoard.ActionPhaseLock);
                GameBoard.gamePhase = 2;
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
                GameBoard.gamePhase = 3;
                Monitor.Wait(GameBoard.BuyPhaseLock);
                GameBoard.gamePhase = 0;
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
