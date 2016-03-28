using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.KingdomCards;
using System.Threading;

namespace DominionCards
{
    public class GameBoard
    {
        public static int gamePhase = 0; // 0 - between phases. 1 - action phase. 2 - buy phase.

        public static Card lastCardPlayed, lastCardBought;
        public static bool AbortPhase = false;
        public static bool AbortGame = false;
        private static GameBoard boardInstance = null;
        public static Object UpdateGraphicsLock = new Object();
        public static Object ActionPhaseLock = new Object();
        public static Object BuyPhaseLock = new Object();

        public Queue<Player> turnOrder;
        public Dictionary<Card, int> cards;
        public GameBoard(Dictionary<Card, int> cards)
        {
            this.cards = cards;
            turnOrder = new Queue<Player>();
            boardInstance = this;
        }

        public static void nullifyInstance()
        {
            boardInstance = null;
        }

        public virtual int getCardsLeft(Card c)
        {
            return cards[c];
        }
        public static void SignalToUpdateGraphics()
        {
            Thread.Sleep(50);
            lock (GameBoard.UpdateGraphicsLock)
            {
                Monitor.PulseAll(GameBoard.UpdateGraphicsLock);
                Monitor.Wait(GameBoard.UpdateGraphicsLock);
            }
        }

        public virtual Player NextPlayer()
        {
            Player nextPlayer = turnOrder.Dequeue();
            turnOrder.Enqueue(nextPlayer);
            return nextPlayer;
        }
        public Boolean AddPlayer(Player p)
        {
            if (turnOrder.Contains(p))
            {
                Console.WriteLine("that player has already been added!");
                return false;
            }
            turnOrder.Enqueue(p);
            return true;
        }
        public void GameRunner()
        {
            try
            {
                Player p = PlayGame();
                string winnerMessage = "Player " + p.getNumber() + " won!";
                Console.WriteLine(winnerMessage);
                System.Windows.Forms.MessageBox.Show(winnerMessage);
            }
            catch (TieException e)
            {
                String str = e.PrintWinners();
                System.Windows.Forms.MessageBox.Show(str + " are tied.");
            }
        }

        public virtual Player PlayGame()
        {
            while (!GameIsOver())
            {
                gamePhase = 0;
                turnOrder.Peek().TakeTurn();
                NextPlayer();
            }
            return FindWinningPlayer();
        }


        private Player currentHightestPlayer;
        private TieException tie;
        private int highestVP, highestMoney; // fields used to compartmentalize FindWinningPlayer()
        public Player FindWinningPlayer()
        {
            Player firstCounted = NextPlayer();
            tie = null;
            currentHightestPlayer = firstCounted;
            highestVP = currentHightestPlayer.countVictoryPoints();
            highestMoney = currentHightestPlayer.getTotalMoney();
            do
            {
                int currentVP = turnOrder.Peek().countVictoryPoints();
                int currentMoney = turnOrder.Peek().getTotalMoney();
                if (tie != null)
                {
                    handleExistngTie(currentVP, currentMoney);
                }
                else if (currentVP > highestVP || (currentVP == highestVP && currentMoney > highestMoney))
                {
                    newLeader(turnOrder.Peek(), currentVP, currentMoney);
                }
                else if (currentVP == highestVP && currentMoney == highestMoney)
                {
                    tie = new TieException(currentHightestPlayer, turnOrder.Peek(), currentVP, currentMoney);
                }
                NextPlayer();
            } while (turnOrder.Peek() != firstCounted);

            if (tie != null)
            {
                throw tie;
            }
            return currentHightestPlayer;
        }

        private void handleExistngTie(int currentVP, int currentMoney)
        {
            if (tie.BreaksTie(turnOrder.Peek()))
            {
                currentHightestPlayer = turnOrder.Peek();
                highestVP = currentVP;
                highestMoney = currentMoney;
                tie = null;
            }
            else if (tie.Ties(turnOrder.Peek()))
            {
                tie.addPlayer(turnOrder.Peek());
            }
        }

        private void newLeader(Player newLeader, int currentVP, int currentMoney)
        {
            currentHightestPlayer = turnOrder.Peek();
            highestMoney = currentMoney;
            highestVP = currentVP;
        }

        public virtual bool GameIsOver()
        {
            if (AbortGame)
            {
                return true;
            }
            Card province = new KingdomCards.Province();
            
            if (cards[province] == 0)
            {
                return true;
            }
            int emptyPiles = 0;
            foreach (Card c in cards.Keys)
            {
                if (cards[c] == 0)
                {
                    emptyPiles++;
                }
            }
            if (emptyPiles >= 3)
            {
                return true;
            }
            return false;
        }
        public Dictionary<Card, int> GetCards()
        {
            return cards;
        }

        public static GameBoard getInstance()
        {
            if (boardInstance == null)
            {
                throw new GameBoardInstanceIsNullException();
            }
            return boardInstance;
        }
    }
}