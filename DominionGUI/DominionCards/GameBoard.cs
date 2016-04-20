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
        private static int gamePhase = 0;
        private static readonly int limboPhaseInt = 0;
        private static Card lastCardPlayed, lastCardBought;
        public static bool AbortPhase = false;
        public static bool AbortGame = false;
        private static GameBoard boardInstance = null;
        public static Object UpdateGraphicsLock = new Object();
        public static Object ActionPhaseLock = new Object();
        public static Object BuyPhaseLock = new Object();
        private GamePhaseEnd endPhase = new GamePhaseEnd();

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
                return false;
            }
            turnOrder.Enqueue(p);
            return true;
        }
        public void GameRunner()
        {
            String toDisplay;
            toDisplay = getVictoryMessage();
            System.Windows.Forms.MessageBox.Show(toDisplay);
        }

        private string getVictoryMessage()
        {
            String toDisplay;
            try
            {
                Player p = PlayGame();
                toDisplay = "Player " + p.getNumber() + " won!";
            }
            catch (TieException e)
            {
                toDisplay = e.PrintWinners() + " are tied";
            }
            return toDisplay;
        }

        public virtual Player PlayGame()
        {
            while (!GameIsOver())
            {
                gamePhase = limboPhaseInt;
                turnOrder.Peek().TakeTurn();
                NextPlayer();
            }
            return FindWinningPlayer();
        }


        private Player currentHighestPlayer;
        private TieException tie;
        private int highestVP, highestMoney; // fields used to compartmentalize FindWinningPlayer()
        public Player FindWinningPlayer()
        {
            Player firstCounted = NextPlayer();
            tie = null;
            currentHighestPlayer = firstCounted;
            endPhase.setEndPhase(currentHighestPlayer);
            highestVP = endPhase.countVictoryPoints();
            highestMoney = endPhase.getTotalMoney();
            do
            {
                Player currentPlayer = turnOrder.Peek();
                endPhase.setEndPhase(currentPlayer);
                int currentVP = endPhase.countVictoryPoints();
                int currentMoney = endPhase.getTotalMoney();
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
                    tie = new TieException(currentHighestPlayer, turnOrder.Peek(), currentVP, currentMoney);
                }
                NextPlayer();
            } while (turnOrder.Peek() != firstCounted);

            if (tie != null)
            {
                throw tie;
            }
            return currentHighestPlayer;
        }

        private void handleExistngTie(int currentVP, int currentMoney)
        {
            if (tie.BreaksTie(turnOrder.Peek()))
            {
                currentHighestPlayer = turnOrder.Peek();
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
            currentHighestPlayer = turnOrder.Peek();
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


        public static int getGamePhase()
        {
            return gamePhase;
        }

        public static void setGamePhase(int passedGamePhase)
        {
            gamePhase = passedGamePhase;
        }

        public static Card getLastCardPlayed()
        {
            return lastCardPlayed;
        }

        public static void setLastCardPlayed(Card passedLastCard)
        {
            lastCardPlayed = passedLastCard;
        }

        public static Card getLastCardBought()
        {
            return lastCardBought;
        }

        public static void setLastCardBought(Card passedBoughtCard)
        {
            lastCardBought = passedBoughtCard;
        }
    }
}