using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DominionCards;
using System.Collections;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestSpy
    {
        GameBoard board;
        Player p1;
        Player p2;
        Card card;

        [TestInitialize]
        public void SetUp()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            board = new GameBoard(dict);
            p1 = new HumanPlayer(1);
            p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            card = new Spy();
            p1.addCardToHand(card);
        }
        [TestMethod]
        public void TestSpyIsActionCard()
        {
            Assert.IsTrue(card.IsAction());
            Assert.IsFalse(card.IsVictory());
            Assert.IsFalse(card.IsTreasure());
        }
        //[TestMethod]
        public void TestSpyWhenDeckEmpty()
        {
            p2.setDiscard(p2.getHand());
            p2.setDeck(new Stack<Card>());
            p1.playCard(card);
            Assert.AreNotEqual(0, p2.getDeck().Count);
            Assert.IsTrue(p2.getDiscard().Count <= 1);
        }

        //[TestMethod]
        public void TestSpyDiscardsPlayerTwosCard()
        {
            int discardCount = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 1, p2.getDiscard().Count);
        }
        //[TestMethod]
        public void TestSpyDoesNotDiscardsPlayerTwosCard()
        {
            int discardCount = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount, p2.getDiscard().Count);
        }
        //[TestMethod]
        public void TestSpyDoesNotDiscardsOwnCard()
        {
            int discardCount = p1.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 1, p1.getDiscard().Count);
        }
        //[TestMethod]
        public void TestSpyDoesDoesDiscardsOwnCard()
        {
            int discardCount = p1.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 2, p1.getDiscard().Count);
        }
    }
}
