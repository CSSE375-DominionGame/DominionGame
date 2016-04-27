using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards
{
    public abstract class Player
    {
        private int number;
        private Stack<Card> deck = new Stack<Card>();
        private List<Card> hand = new List<Card>();
        private List<Card> discard = new List<Card>();
        private Queue<AttackCard> attacks = new Queue<AttackCard>();

        public int actions, buys, money;
        public Player()
        {
            resetStats();
            populateStartingDeck();
            System.Threading.Thread.Sleep(new Random().Next(100));
            deck = ShuffleDiscard();
            drawHand();
        }

        private void resetStats()
        {
            actions = 1;
            buys = 1;
            money = 0;
        }

        private void populateStartingDeck()
        {
            for (int i = 0; i < 3; i++)
            {
                discard.Add(new KingdomCards.Estate());
            }
            for (int i = 0; i < 7; i++)
            {
                discard.Add(new KingdomCards.Copper());
            }
        }
        public void setNumber(int numb)
        {
            number = numb;
        }
        public int getNumber()
        {
            return number;
        }
        public Card GetNextCard()
        {
            if (deck.Count == 0)
            {
                deck = ShuffleDiscard();
            }
            return deck.Pop();
        }
        public void drawHand()
        {
            // discard old hand
            for (int i = 0; i < hand.Count; i++)
            {
                discard.Add((Card)hand[i]);
            }
            hand.Clear();
            // draw five new cards
            for (int i = 0; i < 5; i++)
            {
                hand.Add(GetNextCard());
            }
        }

        public abstract void actionPhase();
        public abstract void buyPhase();
        public abstract List<Card> MakeDecision(IDecision decision);


        public List<Card> getHand()
        {
            return hand;
        }
        public void setHand(List<Card> h)
        {
            hand = h; // THIS METHOD IS FOR TESTING USE
        }
        public void setDiscard(List<Card> dis)
        {
            discard = dis; // THIS METHOD IS USED FOR TESTING
        }
        public void setDeck(Stack<Card> d)
        {
            deck = d; // THIS METHOD IS FOR TESTING USE
        }
        public Stack<Card> getDeck()
        {
            return deck;
        }
        public List<Card> getDiscard()
        {
            return discard;
        }
        public Queue<AttackCard> getAttacks()
        {
            return attacks;
        }
        public int actionsLeft()
        {
            return actions;
        }
        public int buysLeft()
        {
            return buys;
        }
        public int moneyLeft()
        {
            return moneyInHand() + this.money;
        }

        public int moneyInHand()
        {
            int moneyInHand = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                Card card = (Card)hand[i];
                if (card.IsTreasure())
                {
                    moneyInHand += ((TreasureCard)card).value;
                }
            }
            return moneyInHand;
        }

        public bool IsBuyPhase()
        {
            if (GameBoard.AbortPhase || GameBoard.AbortGame)
            {
                GameBoard.AbortPhase = false;
                return false;
            }
            if (buysLeft() == 0)
            {
                return false;
            }
            return true;
        }
        public bool buyCard(Card card)
        {
            if (this.moneyLeft() < card.getPrice())
            {
                return false;
            }
            if (GameBoard.getInstance().getCardsLeft(card) == 0)
            {
                return false;
            }
            discard.Add(card);
            GameBoard.getInstance().GetCards()[card] -= 1;
            buys--;
            this.money -= card.getPrice();
            return true;
        }

        public void addCardToHand(Card card)
        {
            hand.Add(card);
        }
        
        public int playCard(Card c)
        {
            EnsureCardIsPlayable(c);
            RemoveCardFromHand(c);
            ActionCard card = (ActionCard) c;
            actions--;
            for (int i = 0; i < card.cards; i++)
            {
                hand.Add(GetNextCard());
            }
            actions += card.actions;
            buys += card.buys;
            money += card.money;
            card.Play(this);
            return actions;
        }

        public void EnsureCardIsPlayable(Card c)
        {
            if (c.IsVictory())
            {
                throw new CardCannotBePlayedException("you cannot play victory cards!!");
            }
            if (c.IsTreasure())
            {
                throw new CardCannotBePlayedException("You cannot play treasure cards!!");
            }
        }

        public void RemoveCardFromHand(Card c)
        {
            int handSize = hand.Count;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Equals(c))
                {
                    hand.Remove(c);
                    discard.Add(c);
                    break;
                }
            }

            if (handSize - 1 != hand.Count)
            {
                throw new Exception("Tried to play a card not in your hand!!!");
            }
        }
        
        public static Stack<Card> ConvertStackToCardStack(Stack s)
        {
            Stack<Card> deck = new Stack<Card>();
            while (s.Count > 0)
            {
                deck.Push((Card)s.Pop());
            }
            return deck;
        }
        public virtual void TakeTurn()
        {
            ProcessAttacks();
            if (!IsActionPhase())
            {
                GameBoard.setGamePhase(2);
            }
            GameBoard.SignalToUpdateGraphics();
            while (IsActionPhase())
            {
                actionPhase();
                GameBoard.SignalToUpdateGraphics();
            }
            bool buyPhaseTemp = IsBuyPhase();
            while (buyPhaseTemp)
            {
                buyPhase();
                buyPhaseTemp = IsBuyPhase();
                if (buyPhaseTemp) // If it's still the buy phase, immediately update graphics, otherwise, wait for the next player to load.
                {
                    GameBoard.SignalToUpdateGraphics();
                }
            }
            EndTurn();
        }
        public override string ToString()
        {
            return "Player " + number;
        }
        public void ProcessAttacks()
        {
            while (attacks.Count > 0)
            {
                AttackCard card = attacks.Dequeue();
                card.MakeDelayedAttack(this);
            }
        }
        public void EndTurn()
        {
            resetStats();
            drawHand();
        }
        public bool IsActionPhase()
        {
            if (GameBoard.AbortPhase || GameBoard.AbortGame)
            {
                GameBoard.AbortPhase = false;
                return false;
            }
            if (actions == 0)
            {
                return false;
            }
            for (int i = 0; i < this.getHand().Count; i++)
            {
                Card card = (Card)this.getHand()[i];
                if (card.IsAction())
                {
                    return true; // if you still have action cards, it's still the action phase.
                }
            }
            return false;
        }

        public Stack<Card> ShuffleDiscard()
        {
            Random random = new Random();
            int n = discard.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card val = discard[k];
                discard[k] = discard[n];
                discard[n] = val;
            }
            Stack<Card> toReturn = new Stack<Card>(discard);
            discard.Clear();
            return toReturn;
        }

    }
}
