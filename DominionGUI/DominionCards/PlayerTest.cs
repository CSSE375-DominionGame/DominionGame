using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using RandomGenerateCards;

namespace DominionCards
{

    [TestClass]
    class PlayerTest
    {
        /*
        [TestMethod]
        public void TestFiveCardsDrawnEvenIfDeckEmpty()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 0)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            p.drawHand();
            Assert.AreEqual(5, p.getHand().Count);
        }
        
        [TestMethod]
        public void DeckShufflesWhenHandDrawnFromTooSmallDeck_FiveCardsDrawn()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 2)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            p.drawHand();
            Assert.AreEqual(5, p.getHand().Count);
        }
        [TestMethod]
        public void DeckShufflesWhenHandDrawnFromTooSmallDeck_DiscardEmpty()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 2)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            p.drawHand();
            Assert.AreEqual(0, p.getDiscard().Count);
        }
        [TestMethod]
        public void DeckShufflesWhenHandDrawnFromTooSmallDeck_DeckHasCorrectNumbCards()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 2)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            int discardCount = newDiscard.Count;
            int deckCount = p.getDeck().Count;
            int expectedShuffledDeckSize = discardCount - 5 + deckCount;
            p.drawHand();
            Assert.AreEqual(expectedShuffledDeckSize, p.getDeck().Count);
        }
        [TestMethod]
        public void testDiscardGoesToDeckWhenCardIsDrawnAndDeckIsEmpty()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 0)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            int discardSize = newDiscard.Count;
            p.drawCard();
            Assert.AreEqual(discardSize - 1, p.getDeck().Count);
        }
        [TestMethod]
        public void testDiscardGoesAwayWhenDeckIsShuffledDrawingCards()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            ArrayList newDiscard = new ArrayList();
            while (deck.Count > 0)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            int discardSize = newDiscard.Count;
            p.drawCard();
            Assert.AreEqual(0, p.getDiscard().Count);
        }
        [TestMethod]
        public void testDrawHandDiscardsOldHand()
        {
            Player p1 = new HumanPlayer();
            p1.drawHand();
            ArrayList hand = p1.getHand();
            p1.drawHand();
            Assert.AreEqual(hand, p1.getHand());
        }
        [TestMethod]
        public void playingTreasureCardDoesntUseAnAction()
        {
            Player p1 = new HumanPlayer();
            int a = p1.actionsLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Copper());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(a, p1.actionsLeft());
        }
        [TestMethod]
        public void playingTreasureCardDoesntAddBuys()
        {
            Player p1 = new HumanPlayer();
            int b = p1.buysLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Gold());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(b, p1.actionsLeft());
        }
        [TestMethod]
        public void playingTreasureCardDoesntDrawCards()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Gold());
            int cards = hand.Count;
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(cards - 1, p1.actionsLeft());
        }
        [TestMethod]
        public void testCountVictoryPointsCountsBasicVictoryCards(){
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Estate());
            Assert.AreEqual(1, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Duchy());
            Assert.AreEqual(4, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Province());
            Assert.AreEqual(10, p1.countVictoryPoints());
        }
        [TestMethod]
        public void drawFiveCardsWhenDeckRunsOut()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            ArrayList discard = new ArrayList();
            p1.setDeck(deck);
            p1.setDiscard(discard);

            // add 3 Estate cards to the deck
            for (int i = 0; i < 3; i++)
            {
                deck.Push(new KingdomCards.Estate());
            }
            // add 3 copper cards to the discard
            for (int i = 0; i < 3; i++)
            {
                discard.Add(new KingdomCards.Copper());
            }
            p1.drawHand();

            ArrayList hand = p1.getHand();
            Assert.AreEqual(5, hand.Count);
        }
        [TestMethod]
        public void drawCardsStillOnDeckFirstWhenDeckRunsOut()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            ArrayList discard = new ArrayList();
            p1.setDeck(deck);
            p1.setDiscard(discard);

            // add 3 Estate cards to the deck
            for (int i = 0; i < 3; i++)
            {
                deck.Push(new KingdomCards.Estate());
            }
            // add 3 copper cards to the discard
            for (int i = 0; i < 3; i++)
            {
                discard.Add(new KingdomCards.Copper());
            }
            p1.drawHand();

            ArrayList hand = p1.getHand();
            Assert.AreEqual(3, ((Card)hand[0]).getID());
            Assert.AreEqual(3, ((Card)hand[1]).getID());
            Assert.AreEqual(3, ((Card)hand[2]).getID());
            Assert.AreEqual(0, ((Card)hand[3]).getID());
            Assert.AreEqual(0, ((Card)hand[4]).getID());
        }
        [TestMethod]
        public void testCountVictoryPointsWhenCardsInDiscard(){
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            ArrayList discard = p1.getDiscard();
            p1.setDeck(deck);
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Province());
            Assert.AreEqual(6, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Duchy());
            Assert.AreEqual(9, p1.countVictoryPoints());
            discard.Add(new KingdomCards.Duchy());
            Assert.AreEqual(12, p1.countVictoryPoints());
        }
        [TestMethod]
        public void testCountVictoryPointsWhenCardsInHand(){
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            ArrayList hand = new ArrayList();
            p1.setDeck(deck);
            p1.setHand(hand);
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Estate());
            Assert.AreEqual(1, p1.countVictoryPoints());
            deck.Push(new KingdomCards.Province());
            Assert.AreEqual(7, p1.countVictoryPoints());
            hand.Add(new KingdomCards.Province());
            Assert.AreEqual(13, p1.countVictoryPoints());
        }
        [TestMethod]
        public void playingTreasureCardAddsMoney()
        {
            Player p1 = new HumanPlayer();
            int m = p1.moneyLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Silver());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(m + 2, p1.moneyLeft());
        }
        [TestMethod]
        public void testDrawHandDrawsFiveCards()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();

            p1.drawHand();
            Assert.AreEqual(5, hand.Count);
        }
        [TestMethod]
        public void testDrawHandRemovesFiveCardsFromDeck()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            int initialDeck = deck.Count;

            p1.drawHand();
            Assert.AreEqual(initialDeck - 5, deck.Count);
        }

        [TestMethod]
        public void testPlayerStartsWithCorrectNumberOfEstates()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            int numbEstates = 0;
            while (deck.Count > 0)
            {
                if (deck.Pop().getID() == 3)
                {
                    numbEstates++;
                }
            }
            Assert.AreEqual(3, numbEstates);
        }
        [TestMethod]
        public void testPlayerStartsWithCorrectNumberOfCopper()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            int numbCopper = 0;
            while (deck.Count > 0)
            {
                if (deck.Pop().getID() == 0)
                {
                    numbCopper++;
                }
            }
            Assert.AreEqual(7, numbCopper);
        }
        [TestMethod]
        public void testPlayerStartsWithCorrectNumberOfCards()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            Assert.AreEqual(10, deck.Count);
        }
        [TestMethod]
        public void testPlayerStartsWithOnlyEstatesAndCopper()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();
            for (int i = 0; i < hand.Count; i++)
            {
                if (((Card)hand[i]).getID() != 0 && ((Card)hand[i]).getID() != 3)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void testDrawCardMakesDeckSmaller()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            int initialDeckSize = deck.Count;
            p1.drawCard();
            Assert.AreEqual(initialDeckSize - 1, deck.Count);
        }
        [TestMethod]
        public void testDrawCardMakesHandBigger()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();

            hand.Add(new KingdomCards.Smithy());
            int initialDeckSize = hand.Count;
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);

            Assert.AreEqual(initialDeckSize + 2, hand.Count);
        }
        [TestMethod]
        public void playingActionCardReducesActionsByOne()
        {
            Player p1 = new HumanPlayer();
            int a = p1.actionsLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Smithy());
            p1.setHand(hand);
            p1.playCard((Card) hand[0]);
            Assert.AreEqual(a - 1, p1.actionsLeft());
        }
        [TestMethod]
        public void playingCardRemovesCardFromHand()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Woodcutter());
            p1.setHand(hand);
            p1.playCard((Card) hand[0]);
            Assert.AreEqual(0, p1.getHand().Count);
        }
        [TestMethod]
        public void playingCardWithBuysAddsBuys()
        {
            Player p1 = new HumanPlayer();
            int b = p1.buysLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Woodcutter());
            p1.setHand(hand);
            p1.playCard((Card) hand[0]);
            Assert.AreEqual(b+1, p1.buysLeft());
        }
        [TestMethod]
        public void playingCardWithoutBuysDoesntAddBuys()
        {
            Player p1 = new HumanPlayer();
            int b = p1.buysLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Smithy());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(b, p1.buysLeft());
        }
        [TestMethod]
        public void playingCardWithActionsAddsActions()
        {
            Player p1 = new HumanPlayer();
            int a = p1.actionsLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Village());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(a+1, p1.actionsLeft());
        }
        [TestMethod]
        public void playingCardWithMoneyAddsMoney()
        {
            Player p1 = new HumanPlayer();
            int m = p1.moneyLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Festival());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(m+2, p1.moneyLeft());
        }
        [TestMethod]
        public void testShuffledDeckContainsSameCards()
        {
            Stack<Card> deck = new Stack<Card>();
            Dictionary<int, int> count;
            Dictionary<int, int> expct = new Dictionary<int, int>();

            deck.Push(new KingdomCards.Copper());
            deck.Push(new KingdomCards.Copper());
            deck.Push(new KingdomCards.Copper());
            deck.Push(new KingdomCards.Village());
            deck.Push(new KingdomCards.Village());
            deck.Push(new KingdomCards.Smithy());

            count = countCards(deck);
            expct.Add(new KingdomCards.Copper().getID(), 3);
            expct.Add(new KingdomCards.Village().getID(), 2);
            expct.Add(new KingdomCards.Smithy().getID(), 1);

            Console.WriteLine(expct);
            Console.WriteLine(count);

            CollectionAssert.AreEqual(expct, count);
        }
        private static void CompareCounts(Dictionary<Card, int> expect, Dictionary<Card, int> cardCount)
// TODO MARKED FOR DEMOLITION
        {
            foreach (KeyValuePair<Card, int> entry in expect)
            {
                //Assert.AreEqual(entry.Value, cardCount[entry.Key]);
                Console.WriteLine("expected: " + entry.Key.getID() + ", " + entry.Value);
                if (cardCount.ContainsKey(entry.Key))
                {
                    Console.WriteLine("returned: " + entry.Key.getID() + ", " + cardCount[entry.Key]);
                }
            }
        }

        [TestMethod]
        public void testConvertStackToCardStack()
        {
            Stack objStack = new Stack();
            Stack<Card> dumpStack;
            Stack<Card> expct = new Stack<Card>();
            expct.Push(new KingdomCards.Copper());
            expct.Push(new KingdomCards.Copper());
            expct.Push(new KingdomCards.Duchy());
            expct.Push(new KingdomCards.Smithy());
            objStack.Push(new KingdomCards.Smithy());
            objStack.Push(new KingdomCards.Duchy());
            objStack.Push(new KingdomCards.Copper());
            objStack.Push(new KingdomCards.Copper());

            dumpStack = Player.ConvertStackToCardStack(objStack);

            while (expct.Count > 0)
            {
                Assert.AreEqual(expct.Pop().getID(), dumpStack.Pop().getID());
            }
            Assert.AreEqual(expct.Count, dumpStack.Count);
        }

        [TestMethod]
        public void playingCardWithoutMoneyDoesntAddMoney()
        {
            Player p1 = new HumanPlayer();
            int m = p1.moneyLeft();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Chapel());
            p1.setHand(hand);
            
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(m, p1.moneyLeft());
        }
        [TestMethod]
        public void playingCardThatDrawsCards()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = new ArrayList();
            hand.Add(new KingdomCards.Laboratory());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(2, hand.Count);
        }

        [TestMethod]
        public void buyingACard()
        {
            Player p1 = new HumanPlayer();
            p1.setDiscard(new ArrayList());
            ArrayList discard = new ArrayList();
            Card laboratory = new KingdomCards.Laboratory();
            discard.Add(laboratory);
            p1.buyCard(laboratory);
            Assert.AreEqual(discard, p1.getDeck());
        }

        [TestMethod]
        public void addCardToHand()
        {
            Player p1 = new HumanPlayer();
            p1.setHand(new ArrayList());
            ArrayList hand = new ArrayList();
            Card laboratory = new KingdomCards.Laboratory();
            hand.Add(laboratory);
            p1.addCardToHand(laboratory);
            Assert.AreEqual(hand, p1.getHand());
        }
        private void printCardStats(ActionCard c)
        {
            Console.WriteLine("cards drawn " + c.cards);
            Console.WriteLine("buys gianed " + c.buys);
            Console.WriteLine("acts gianed " + c.actions);
            Console.WriteLine("cash gianed " + c.money);
            Console.Read();
            Console.WriteLine();
        }
        [TestMethod]
        public void testCountCards()
        {
            Stack<Card> deck = new Stack<Card>();
            deck.Push(new KingdomCards.Cellar());
            deck.Push(new KingdomCards.Village());
            deck.Push(new KingdomCards.Smithy());
            deck.Push(new KingdomCards.Village());
            deck.Push(new KingdomCards.Village());

            Dictionary<int, int> count = countCards(deck);
            Dictionary<int, int> expct = new Dictionary<int, int>();
            expct.Add(new KingdomCards.Village().getID(), 3);
            expct.Add(new KingdomCards.Smithy().getID(), 1);
            expct.Add(new KingdomCards.Cellar().getID(), 1);

            CollectionAssert.AreEqual(count, expct);
        }

        private Dictionary<int, int> countCards(Stack<Card> deck)
        {
            Dictionary<int, int> count = new Dictionary<int, int>();
            Stack<Card> temp = new Stack<Card>();

            while (deck.Count > 0)
            {
                Card c = deck.Pop();
                if (count.ContainsKey(c.getID()))
                {
                    int number = count[c.getID()] + 1;
                    count.Remove(c.getID());
                    count.Add(c.getID(), number);
                }
                else
                {
                    count.Add(c.getID(), 1);
                }
            }
            // reconstruct the deck
            while (temp.Count > 0)
            {
                deck.Push(temp.Pop());
            }
            return count;
        }
         */
    }
}
