using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using DominionCards;
using DominionCards.KingdomCards;
using System.Threading;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestPlayer
    {
        private static Dictionary<Card, int> GetTestCards()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards[new Gold()] = 30;
            cards[new Silver()] = 40;
            cards[new Copper()] = 60;
            cards[new Province()] = 12;
            cards[new Duchy()] = 12;
            cards[new Estate()] = 12;
            cards[new Curse()] = 30;
            cards[new Cellar()] = 10;
            cards[new Moat()] = 10;
            cards[new Woodcutter()] = 10;
            cards[new Village()] = 10;
            cards[new Militia()] = 10;
            cards[new Mine()] = 10;
            cards[new Remodel()] = 10;
            cards[new Feast()] = 10;
            cards[new Workshop()] = 10;
            cards[new Market()] = 10;
            return cards;
        }
        [TestMethod]
        public void TestPlayerToString()
        {
            for (int i = 0; i < 4; i++)
            {
                Player p = new HumanPlayer(i + 1);
                Assert.AreEqual("Player " + (i + 1), p.ToString());
            }
        }
        [TestMethod]
        public void TestEndTurnResetsPlayerActions()
        {
            Player p1 = new HumanPlayer();
            p1.actions = 0;
            p1.EndTurn();
            Assert.AreEqual(1, p1.actionsLeft());
        }
        [TestMethod]
        public void TestEndTurnResetsPlayerBuys()
        {
            Player p1 = new HumanPlayer();
            p1.buys = 0;
            p1.EndTurn();
            Assert.AreEqual(1, p1.buysLeft());
        }
        [TestMethod]
        public void TestEndTurnResetsDrawsNewHand()
        {
            Player p1 = new HumanPlayer();
            p1.setHand(new List<Card>());
            p1.EndTurn();
            Assert.AreEqual(5, p1.getHand().Count);
        }
        [TestMethod]
        public void TestEndTurnResetsPlayerMoney()
        {
            Player p1 = new HumanPlayer();
            p1.money = 9;
            p1.EndTurn();
            Assert.AreEqual(0, p1.money);
        }
        [TestMethod]
        public void TestEndTurnDiscardsOldHand()
        {
            Player p1 = new HumanPlayer();
            int discard = p1.getDiscard().Count;
            int handSize = p1.getHand().Count;
            p1.EndTurn();
            Assert.AreEqual(discard + handSize, p1.getDiscard().Count);
        }
        [TestMethod]
        public void TestIsBuyPhaseIsFalseWithNoBuys()
        {
            Player p1 = new HumanPlayer(1);
            p1.buys = 0;
            Assert.IsFalse(p1.IsBuyPhase());
        }
        [TestMethod]
        public void TestIsBuyPhaseIsTrueWithBuys()
        {
            GameBoard board = new GameBoard(new Dictionary<Card, int>());
            Player p1 = new HumanPlayer(1);
            Assert.IsTrue(p1.IsBuyPhase());
        }
        [TestMethod]
        public void TestIsActionPhaseFailsWithNoActions()
        {
            Player p1 = new HumanPlayer();
            p1.addCardToHand(new Village());
            p1.actions = 0;
            Assert.IsFalse(p1.IsActionPhase());
        }
        [TestMethod]
        public void TestIsActionPhaseFailsWithNoActionCards()
        {
            Player p1 = new HumanPlayer();
            p1.setHand(new List<Card>());
            p1.actions = 1;
            Assert.IsFalse(p1.IsActionPhase());
        }
        [TestMethod]
        public void TestIsActionPhasePassesWithActionCardsAndActionsLeft()
        {
            Player p1 = new HumanPlayer();
            p1.addCardToHand(new Village());
            p1.actions = 1;
            Assert.IsTrue(p1.IsActionPhase());
        }
        [TestMethod]
        public void BuyFailsIfNoCardsLeftToBuy()
        {
            GameBoard board = new GameBoard(GetTestCards());
            Player p1 = new HumanPlayer(1);
            p1.addCardToHand(new Gold());
            board.GetCards()[new Silver()] = 0;
            board.AddPlayer(p1);
            Assert.IsFalse(p1.buyCard(new Silver()));
        }
        [TestMethod]
        public void IntegrationFailsToBuyCardWithNoneLeft()
        {
            GameBoard board = new GameBoard(GetTestCards());
            board.GetCards()[new Gold()] = 0;
            Player p1 = new HumanPlayer(1);
            Assert.IsFalse(p1.buyCard(new Gold()));
        }
        [TestMethod]
        public void IntegrationTestBuyCardAddsNewCardToDiscard()
        {
            GameBoard board = new GameBoard(GetTestCards());
            Player p1 = new HumanPlayer(1);
            p1.addCardToHand(new Gold()); // ensures player has enough money to buy a silver.
            int discardSize = p1.getDiscard().Count;
            p1.buyCard(new Silver());
            Assert.IsTrue(p1.getDiscard().Contains(new Silver()));
            Assert.AreEqual(discardSize + 1, p1.getDiscard().Count);
        }
        [TestMethod]
        public void IntegrationFailsToBuyCardWithoutEnoughMoney()
        {
            GameBoard board = new GameBoard(GetTestCards());
            Player p1 = new HumanPlayer(1);
            Assert.IsFalse(p1.buyCard(new Province()));
        }
        [TestMethod]
        public void TestThatActionPhasePhaseDoesNotPlayCardWithoutEnoughActions()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Witch(), 10);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);

            p1.actions = 0;
            p1.addCardToHand(new Witch());
            p1.addCardToHand(new Copper());
            GameBoard.setLastCardPlayed(new Copper());

            Thread t = new Thread(p1.actionPhase);
            t.Start();
            GameBoard.setLastCardPlayed(new Witch());

            Assert.IsFalse(p2.getDiscard().Contains(new Witch()));
        }
        //[TestMethod]
        public void TestThatActionPhasePlaysCardLastClicked()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Witch(), 10);
            dict.Add(new Curse(), 10);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            GameBoard.setLastCardPlayed(new Copper());
            Card witch = new Witch();
            p1.addCardToHand(witch);
            p1.addCardToHand(new Copper());

            Thread t = new Thread(new ThreadStart(p1.actionPhase));
            t.Start();
            Console.WriteLine("TEST: thread launched successfully. Entering 5 second sleep.");
            Thread.Sleep(100);
            Console.WriteLine("TEST: woke up. Setting 'CardPlayed' to witch.");
            GameBoard.setLastCardPlayed(witch);
            Console.Write("about to enter sync block.....");

            lock (GameBoard.ActionPhaseLock)
            {
                Console.WriteLine("Entering sync block");
                Monitor.PulseAll(GameBoard.ActionPhaseLock);
                Console.WriteLine("Button pressed!");
                Monitor.Wait(GameBoard.ActionPhaseLock);
                Console.WriteLine("finished waiting.");
            }
            Assert.IsTrue(p2.getDiscard().Contains(new Curse()));
            Assert.IsTrue(p1.getDiscard().Contains(new Witch()));
        }
        [TestMethod]
        public void TestThatActionPhaseEndsIfNullCardClicked()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Witch(), 10);
            dict.Add(new Curse(), 10);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            int actionsBefore = p1.actionsLeft();
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            GameBoard.setLastCardPlayed(new Copper());
            Card witch = new Witch();
            p1.addCardToHand(witch);
            p1.addCardToHand(new Copper());

            Thread t = new Thread(new ThreadStart(p1.actionPhase));
            t.Start();

            Thread.Sleep(100);
            lock (GameBoard.ActionPhaseLock)
            {
                Monitor.PulseAll(GameBoard.ActionPhaseLock);
                Monitor.Wait(GameBoard.ActionPhaseLock);
            }
            Assert.AreEqual(actionsBefore, p1.actionsLeft());
            Assert.IsFalse(p2.getDiscard().Contains(new Curse()));
            Assert.IsFalse(p1.getDiscard().Contains(new Witch()));
        }
        [TestMethod]
        public void BuyPhaseBuysLastCardClicked()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Copper(), 60);
            dict.Add(new Estate(), 12);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            p1.addCardToHand(new Gold());
            int discardSize = p1.getDiscard().Count;
            int moneyLeft = p1.moneyLeft();

            GameBoard.setLastCardBought(new Copper());

            Thread t = new Thread(p1.buyPhase);
            t.Start();

            Thread.Sleep(100);
            GameBoard.setLastCardBought(new Estate());
            lock (GameBoard.BuyPhaseLock)
            {
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                Monitor.Wait(GameBoard.BuyPhaseLock);
            }
            Assert.IsTrue(p1.getDiscard().Contains(new Estate()));
            Assert.IsFalse(p1.getDiscard().Contains(new Copper()));
            Assert.AreEqual(discardSize + 1, p1.getDiscard().Count);
            Assert.AreEqual(moneyLeft - new Estate().getPrice(), p1.moneyLeft());
        }
        //[TestMethod]
        public void BuyPhaseFailsWithoutEnoughMoney()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Copper(), 60);
            dict.Add(new Province(), 12);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            int discardSize = p1.getDiscard().Count;
            int moneyLeft = p1.moneyLeft();

            GameBoard.setLastCardBought(new Copper());

            Thread t = new Thread(p1.buyPhase);
            t.Start();

            Thread.Sleep(100);
            GameBoard.setLastCardBought(new Province());
            lock (GameBoard.BuyPhaseLock)
            {
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                Monitor.Wait(GameBoard.BuyPhaseLock);
            }
            Assert.IsFalse(p1.getDiscard().Contains(new Province()));
            Assert.IsFalse(p1.getDiscard().Contains(new Copper()));
            Assert.AreEqual(discardSize, p1.getDiscard().Count);
            Assert.AreEqual(moneyLeft, p1.moneyLeft());
        }
        //[TestMethod]
        public void BuyPhaseBuysFailsIfNoCardsLeft()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Copper(), 60);
            dict.Add(new Estate(), 0);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            p1.addCardToHand(new Gold());
            int discardSize = p1.getDiscard().Count;
            int moneyLeft = p1.moneyLeft();

            GameBoard.setLastCardBought(new Copper());

            Thread t = new Thread(p1.buyPhase);
            t.Start();

            Thread.Sleep(100);
            GameBoard.setLastCardBought(new Estate());
            lock (GameBoard.BuyPhaseLock)
            {
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                Monitor.Wait(GameBoard.BuyPhaseLock);
            }
            Assert.IsFalse(p1.getDiscard().Contains(new Estate()));
            Assert.IsFalse(p1.getDiscard().Contains(new Copper()));
            Assert.AreEqual(discardSize, p1.getDiscard().Count);
            Assert.AreEqual(moneyLeft, p1.moneyLeft());
        }
        [TestMethod]
        public void BuyPhaseExitsIfCardBoughtIsNull()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Copper(), 60);
            dict.Add(new Estate(), 12);
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            p1.addCardToHand(new Gold());
            int discardSize = p1.getDiscard().Count;
            int moneyLeft = p1.moneyLeft();

            GameBoard.setLastCardBought(new Copper());

            Thread t = new Thread(p1.buyPhase);
            t.Start();

            Thread.Sleep(100);
            lock (GameBoard.BuyPhaseLock)
            {
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                Monitor.Wait(GameBoard.BuyPhaseLock);
            }
            Assert.IsFalse(p1.getDiscard().Contains(new Estate()));
            Assert.IsFalse(p1.getDiscard().Contains(new Copper()));
            Assert.AreEqual(discardSize, p1.getDiscard().Count);
            Assert.AreEqual(moneyLeft, p1.moneyLeft());
        }
        [TestMethod]
        public void TestIsActionPhaseIsFalseWithoutActionCard()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(dict);
            HumanPlayer p1 = new HumanPlayer(1);
            List<Card> hand = new List<Card>();
            hand.Add(new Estate());
            p1.setHand(hand);

            Assert.IsFalse(p1.IsActionPhase());
        }
        [TestMethod]
        public void TestIsActionPhaseIsFalseWithoutActionsLeft()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(dict);
            HumanPlayer p1 = new HumanPlayer(1);
            p1.actions = 0;
            List<Card> hand = new List<Card>();
            hand.Add(new Village());
            p1.setHand(hand);

            Assert.IsFalse(p1.IsActionPhase());
        }
        [TestMethod]
        public void TestIsActionPhaseIsFalseWithoutAction()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(dict);
            HumanPlayer p1 = new HumanPlayer(1);
            List<Card> hand = new List<Card>();
            hand.Add(new Estate());
            hand.Add(new Witch());
            p1.actions = 0;
            p1.setHand(hand);

            Assert.IsFalse(p1.IsActionPhase());
        }
        [TestMethod]
        public void TestIsActionPhaseIsTrueWithActionsAndCards()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(dict);
            HumanPlayer p1 = new HumanPlayer(1);
            List<Card> hand = new List<Card>();
            hand.Add(new Estate());
            hand.Add(new Witch());
            p1.setHand(hand);

            Assert.IsTrue(p1.IsActionPhase());
        }

        [TestMethod]
        public void TestFiveCardsDrawnEvenIfDeckEmpty()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            List<Card> newDiscard = new List<Card>();
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
            List<Card> newDiscard = new List<Card>();
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
            List<Card> newDiscard = new List<Card>();
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
            Player p1 = new HumanPlayer();
            Stack<Card> deck = p1.getDeck();
            List<Card> hand = p1.getHand();
            List<Card> newDiscard = new List<Card>();
            while (deck.Count > 2)
            {
                newDiscard.Add(deck.Pop());
            }
            for (int i = 0; i < hand.Count; i++)
            {
                newDiscard.Add(hand[i]);
            }
            p1.setHand(new List<Card>());
            p1.setDiscard(newDiscard);
            int discardCount = newDiscard.Count;
            int deckCount = p1.getDeck().Count;
            int expectedShuffledDeckSize = discardCount - 5 + deckCount;
            p1.drawHand();
            Assert.AreEqual(expectedShuffledDeckSize, p1.getDeck().Count);
        }
        [TestMethod]
        public void testDiscardGoesToDeckWhenCardIsDrawnAndDeckIsEmpty()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            List<Card> newDiscard = new List<Card>();
            while (deck.Count > 0)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            int discardSize = newDiscard.Count;
            p.GetNextCard();
            Assert.AreEqual(discardSize - 1, p.getDeck().Count);
        }
        [TestMethod]
        public void testDiscardGoesAwayWhenDeckIsShuffledDrawingCards()
        {
            Player p = new HumanPlayer();
            Stack<Card> deck = p.getDeck();
            List<Card> newDiscard = new List<Card>();
            while (deck.Count > 0)
            {
                newDiscard.Add(deck.Pop());
            }
            p.setDiscard(newDiscard);
            int discardSize = newDiscard.Count;
            p.GetNextCard();
            Assert.AreEqual(0, p.getDiscard().Count);
        }
        [TestMethod]
        public void testDrawHandDiscardsOldHand()
        {
            Player p1 = new HumanPlayer();
            p1.drawHand();
            List<Card> hand = p1.getHand();
            p1.drawHand();
            Assert.AreEqual(hand, p1.getHand());
        }
        [TestMethod]
        public void testCountVictoryPointsCountsBasicVictoryCards()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new Estate());
            Assert.AreEqual(1, p1.countVictoryPoints());
            deck.Push(new Duchy());
            Assert.AreEqual(4, p1.countVictoryPoints());
            deck.Push(new Province());
            Assert.AreEqual(10, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountVictoryPointsCountsGardens()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            for (int i = 0; i < 9; i++)
            {
                deck.Push(new Militia());
            }
            deck.Push(new Gardens());
            Assert.AreEqual(1, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountVictoryPointsCountsGardensRoundsDown()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            for (int i = 0; i < 8; i++)
            {
                deck.Push(new Militia());
            }
            deck.Push(new Gardens());
            Assert.AreEqual(0, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountVictoryPointsCountsGardensDoesntRoundUp()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            for (int i = 0; i < 11; i++)
            {
                deck.Push(new Militia());
            }
            deck.Push(new Gardens());
            Assert.AreEqual(1, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountVictoryPointsCountsGardensCanBeMoreThanOne()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            for (int i = 0; i < 22; i++)
            {
                deck.Push(new Militia());
            }
            deck.Push(new Gardens());
            Assert.AreEqual(2, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountVictoryPointsCountsGardensWithMultipleGardens()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setDeck(deck);
            p1.setHand(new List<Card>());
            for (int i = 0; i < 9; i++)
            {
                deck.Push(new Militia());
            }
            deck.Push(new Gardens());
            deck.Push(new Gardens());
            Assert.AreEqual(2, p1.countVictoryPoints());
        }

        [TestMethod]
        public void drawFiveCardsWhenDeckRunsOut()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            List<Card> discard = new List<Card>();
            p1.setDeck(deck);
            p1.setDiscard(discard);

            // add 3 Estate cards to the deck
            for (int i = 0; i < 3; i++)
            {
                deck.Push(new Estate());
            }
            // add 3 copper cards to the discard
            for (int i = 0; i < 3; i++)
            {
                discard.Add(new Copper());
            }
            p1.drawHand();

            List<Card> hand = p1.getHand();
            Assert.AreEqual(5, hand.Count);
        }
        [TestMethod]
        public void drawCardsStillOnDeckFirstWhenDeckRunsOut()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            List<Card> discard = new List<Card>();
            List<Card> hand = new List<Card>();

            deck.Push(new Witch());
            deck.Push(new Gold());

            for (int i = 0; i < 5; i++)
            {
                discard.Add(new Copper());
            }

            p1.setHand(hand);
            p1.setDeck(deck);
            p1.setDiscard(discard);

            p1.drawHand();

            hand = p1.getHand();
            Assert.AreEqual(new Gold(), hand[0]);
            Assert.AreEqual(new Witch(), hand[1]);
        }
        [TestMethod]
        public void testCountVictoryPointsWhenCardsInDiscard()
        {
            Player p1 = new HumanPlayer();
            p1.setHand(new List<Card>());
            Stack<Card> deck = new Stack<Card>();
            List<Card> discard = p1.getDiscard();
            p1.setDeck(deck);
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new Province());
            Assert.AreEqual(6, p1.countVictoryPoints());
            deck.Push(new Duchy());
            Assert.AreEqual(9, p1.countVictoryPoints());
            discard.Add(new Duchy());
            Assert.AreEqual(12, p1.countVictoryPoints());
        }
        [TestMethod]
        public void testCountVictoryPointsWhenCardsInHand()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            List<Card> hand = new List<Card>();
            p1.setDeck(deck);
            p1.setHand(hand);
            Assert.AreEqual(0, p1.countVictoryPoints());
            deck.Push(new Estate());
            Assert.AreEqual(1, p1.countVictoryPoints());
            deck.Push(new Province());
            Assert.AreEqual(7, p1.countVictoryPoints());
            hand.Add(new Province());
            Assert.AreEqual(13, p1.countVictoryPoints());
        }

        [TestMethod]
        public void testCountTotalMoneyinDeck()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setHand(new List<Card>());
            deck.Push(new Gold());
            p1.setDeck(deck);
            Assert.AreEqual(3, p1.getTotalMoney());
        }

        [TestMethod]
        public void testCountTotalMoneyWithCardsinDiscard()
        {
            Player p1 = new HumanPlayer();
            Stack<Card> deck = new Stack<Card>();
            p1.setHand(new List<Card>());
            List<Card> discard = p1.getDiscard();
            discard.Add(new Gold());
            p1.setDeck(deck);
            Assert.AreEqual(3, p1.getTotalMoney());
        }
        [TestMethod]
        public void HavingTreasureCardAddsMoney()
        {
            Player p1 = new HumanPlayer();
            int m = p1.moneyLeft();
            p1.addCardToHand(new Silver());
            Assert.AreEqual(m + 2, p1.moneyLeft());
        }
        [TestMethod]
        public void testDrawHandDrawsFiveCards()
        {
            Player p1 = new HumanPlayer();
            List<Card> hand = p1.getHand();

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
            List<Card> hand = p1.getHand();
            int numbEstates = 0;
            while (deck.Count > 0)
            {
                if (deck.Pop().getID() == 3)
                {
                    numbEstates++;
                }
            }
            for (int i = 0; i < hand.Count; i++)
            {
                int id = ((Card)hand[i]).getID();
                if (id == 3)
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
            for (int i = 0; i < p1.getHand().Count; i++)
            {
                int id = ((Card)p1.getHand()[i]).getID();
                if (id == 0)
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
            Assert.AreEqual(5, deck.Count);
            Assert.AreEqual(5, p1.getHand().Count);
        }
        [TestMethod]
        public void testPlayerStartsWithOnlyEstatesAndCopper()
        {
            Player p1 = new HumanPlayer();
            List<Card> hand = p1.getHand();
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
            p1.GetNextCard();
            Assert.AreEqual(initialDeckSize - 1, deck.Count);
        }
        [TestMethod]
        public void testDrawCardMakesHandBigger()
        {
            Player p1 = new HumanPlayer();
            List<Card> hand = p1.getHand();

            Card c = new Smithy();
            hand.Add(c);
            int initialDeckSize = hand.Count;
            p1.setHand(hand);
            p1.playCard(c);

            Assert.AreEqual(initialDeckSize + 2, hand.Count);
        }
        [TestMethod]
        public void playingActionCardReducesActionsByOne()
        {
            Player p1 = new HumanPlayer();
            int a = p1.actionsLeft();
            List<Card> hand = new List<Card>();
            hand.Add(new Smithy());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(a - 1, p1.actionsLeft());
        }
        [TestMethod]
        public void playingCardRemovesCardFromHand()
        {
            Player p1 = new HumanPlayer();
            List<Card> hand = new List<Card>();
            hand.Add(new Woodcutter());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(0, p1.getHand().Count);
        }
        [TestMethod]
        public void playingCardWithBuysAddsBuys()
        {
            Player p1 = new HumanPlayer();
            int b = p1.buysLeft();
            List<Card> hand = new List<Card>();
            hand.Add(new Woodcutter());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(b + 1, p1.buysLeft());
        }
        [TestMethod]
        public void playingCardWithoutBuysDoesntAddBuys()
        {
            Player p1 = new HumanPlayer();
            int b = p1.buysLeft();
            List<Card> hand = new List<Card>();
            hand.Add(new Smithy());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(b, p1.buysLeft());
        }
        [TestMethod]
        public void playingCardWithActionsAddsActions()
        {
            Player p1 = new HumanPlayer();
            int a = p1.actionsLeft();
            List<Card> hand = new List<Card>();
            hand.Add(new Village());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(a + 1, p1.actionsLeft());
        }
        [TestMethod]
        public void playingCardWithMoneyAddsMoney()
        {
            Player p1 = new HumanPlayer();
            int m = p1.moneyLeft();
            p1.addCardToHand(new Festival());
            p1.playCard(new Festival());
            Assert.AreEqual(m + 2, p1.moneyLeft());
        }
        [TestMethod]
        public void testShuffledDeckContainsSameCards()
        {
            Stack<Card> deck = new Stack<Card>();
            Dictionary<int, int> count;
            Dictionary<int, int> expct = new Dictionary<int, int>();

            deck.Push(new Copper());
            deck.Push(new Copper());
            deck.Push(new Copper());
            deck.Push(new Village());
            deck.Push(new Village());
            deck.Push(new Smithy());

            count = countCards(deck);
            expct.Add(new Copper().getID(), 3);
            expct.Add(new Village().getID(), 2);
            expct.Add(new Smithy().getID(), 1);

            Console.WriteLine(expct);
            Console.WriteLine(count);

            foreach (int id in count.Keys)
            {
                Assert.AreEqual(expct[id], count[id]);
            }
        }

        [TestMethod]
        public void testConvertStackToCardStack()
        {
            Stack objStack = new Stack();
            Stack<Card> dumpStack;
            Stack<Card> expct = new Stack<Card>();
            expct.Push(new Copper());
            expct.Push(new Copper());
            expct.Push(new Duchy());
            expct.Push(new Smithy());
            objStack.Push(new Smithy());
            objStack.Push(new Duchy());
            objStack.Push(new Copper());
            objStack.Push(new Copper());

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
            Stack<Card> deck = new Stack<Card>();
            deck.Push(new Estate());
            deck.Push(new Estate());
            deck.Push(new Estate());
            deck.Push(new Estate());
            Player p1 = new HumanPlayer();
            p1.setDeck(deck);
            int m = p1.moneyLeft();
            p1.addCardToHand(new Laboratory());
            p1.playCard(new Laboratory());
            Assert.AreEqual(m, p1.moneyLeft());
        }
        [TestMethod]
        public void playingCardThatDrawsCards()
        {
            Player p1 = new HumanPlayer();
            List<Card> hand = new List<Card>();
            hand.Add(new Laboratory());
            p1.setHand(hand);
            p1.playCard((Card)hand[0]);
            Assert.AreEqual(2, hand.Count);
        }

        [TestMethod]
        public void buyingAddsCardToDiscard()
        {
            Player p1 = new HumanPlayer();
            List<Card> discard = new List<Card>();
            p1.setDiscard(discard);
            Card laboratory = new Laboratory();
            int discardSize = discard.Count;
            p1.buyCard(laboratory);
            Assert.AreEqual(discardSize, p1.getDiscard().Count);
        }

        [TestMethod]
        public void testBuyLowersNumBuysWithEnoughMoney()
        {
            Dictionary<Card, int> dict = new Dictionary<Card,int>();
            dict[new Laboratory()] = 1;
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer();
            int originalBuys = p1.buysLeft();
            Card purchase = new Laboratory();  //5
            List<Card> tempHand = new List<Card>();
            Card gold1 = new Gold();
            Card gold2 = new Gold();
            tempHand.Add(gold1);
            tempHand.Add(gold2);
            p1.setHand(tempHand);
            p1.buyCard(purchase);
            int buysAfter = p1.buysLeft();
            Assert.AreEqual((originalBuys - 1), buysAfter);
        }

        [TestMethod]
        public void testBuyLowersNumBuysNotEnoughMoney()
        {
            Player p1 = new HumanPlayer();
            int originalBuys = p1.buysLeft();
            Card purchase = new Laboratory();  //5
            p1.buyCard(purchase);
            int buysAfter = p1.buysLeft();
            Assert.AreEqual(originalBuys, buysAfter);
        }

        [TestMethod]
        public void buyingExpensiveCardTakesMoneyFromPlayer()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict[new Laboratory()] = 1;
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer();
            Card purchase = new Laboratory();  //5
            List<Card> tempHand = new List<Card>();
            Card gold1 = new Gold();
            Card gold2 = new Gold();
            tempHand.Add(gold1);
            tempHand.Add(gold2);
            p1.setHand(tempHand);
            int moneyBefore = p1.moneyLeft();
            int cost = purchase.getPrice();
            p1.buyCard(purchase);
            Assert.AreEqual((moneyBefore - cost), p1.moneyLeft());


        }
        [TestMethod]
        public void buyingCheapCardTakesMoneyFromPlayer()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict[new Estate()] = 1;
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer();
            Card purchase = new Estate();  //2
            List<Card> tempHand = new List<Card>();
            Card gold1 = new Gold();
            Card gold2 = new Gold();
            tempHand.Add(gold1);
            tempHand.Add(gold2);
            p1.setHand(tempHand);
            int moneyBefore = p1.moneyLeft();
            int cost = purchase.getPrice();
            p1.buyCard(purchase);
            Assert.AreEqual((moneyBefore - cost), p1.moneyLeft());
        }

        [TestMethod]
        public void testIfUserHasTooLittleMoney()
        {
            Player p1 = new HumanPlayer();
            Card purchase = new Laboratory();  //5
            Assert.IsFalse(p1.buyCard(purchase));
        }


        [TestMethod]
        public void testIfUserHasEnoughMoney()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict[new Laboratory()] = 1;
            GameBoard board = new GameBoard(dict);
            Player p1 = new HumanPlayer();
            Card purchase = new Laboratory();  //5
            List<Card> tempHand = new List<Card>();
            Card gold1 = new Gold();
            Card gold2 = new Gold();
            tempHand.Add(gold1);
            tempHand.Add(gold2);
            p1.setHand(tempHand);
            Assert.IsTrue(p1.buyCard(purchase));
        }

        [TestMethod]
        public void cardNotAddedWithNotEnoughMoney()
        {
            Player p1 = new HumanPlayer();
            Card purchase = new Laboratory();
            int discardSize = p1.getDiscard().Count;
            p1.buyCard(purchase);
            Assert.AreEqual(discardSize, p1.getDiscard().Count);
        }

        [TestMethod]
        public void addCardToHand()
        {
            Player p1 = new HumanPlayer();
            p1.setHand(new List<Card>());
            int handCountBefore = p1.getHand().Count;
            p1.addCardToHand(new Library());
            Assert.AreEqual(handCountBefore + 1, p1.getHand().Count);
            Assert.IsTrue(p1.getHand().Contains(new Library()));
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
            deck.Push(new Cellar());
            deck.Push(new Village());
            deck.Push(new Smithy());
            deck.Push(new Village());
            deck.Push(new Village());

            Dictionary<int, int> count = countCards(deck);
            Dictionary<int, int> expct = new Dictionary<int, int>();
            expct.Add(new Village().getID(), 3);
            expct.Add(new Smithy().getID(), 1);
            expct.Add(new Cellar().getID(), 1);

            CollectionAssert.AreEqual(count, expct);
        }

        [TestMethod]
        public void testPlayingAttackMakesOpponentQueueBigger()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            Player p4 = new HumanPlayer(4);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            board.AddPlayer(p4);
            int p2AttackSize = p2.getAttacks().Count;
            int p3AttackSize = p3.getAttacks().Count;
            int p4AttackSize = p4.getAttacks().Count;
            Card militia = new Militia();
            p1.getHand().Add(militia);
            p1.playCard(militia);
            Assert.AreEqual(p2AttackSize + 1, p2.getAttacks().Count);
            Assert.AreEqual(p3AttackSize + 1, p3.getAttacks().Count);
            Assert.AreEqual(p4AttackSize + 1, p4.getAttacks().Count);
        }

        //[TestMethod]
        public void TestPlayerIgnoresAttacksWithMoat()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int p2AttackSize = p2.getAttacks().Count;
            int p3AttackSize = p3.getAttacks().Count;
            Card militia = new Militia();
            p1.getHand().Add(militia);
            p2.getHand().Add(new Moat());
            p1.playCard(militia);
            Assert.AreEqual(p2AttackSize, p2.getAttacks().Count);
            Assert.AreEqual(p3AttackSize + 1, p3.getAttacks().Count);
        }

        [TestMethod]
        public void TestPlayerIsSameBeforeAndAfterAttack()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int p2AttackSize = p2.getAttacks().Count;
            int p3AttackSize = p3.getAttacks().Count;
            Card militia = new Militia();
            p1.getHand().Add(militia);
            Player before = board.turnOrder.Peek();
            p1.playCard(militia);
            Assert.AreSame(before, board.turnOrder.Peek());

        }

        [TestMethod]
        public void testPlayingAttackDoesNotMakeYourQueueBigger()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            Player p4 = new HumanPlayer(4);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            board.AddPlayer(p4);
            int p1AttackSize = p1.getAttacks().Count;
            Card militia = new Militia();
            p1.getHand().Add(militia);
            p1.playCard(militia);
            Assert.AreEqual(p1AttackSize, p1.getAttacks().Count);
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


    }
}
