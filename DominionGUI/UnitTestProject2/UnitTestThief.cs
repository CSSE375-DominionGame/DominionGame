using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using System.Collections.Generic;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestThief
    {
        Player p1;
        Player p2;
        Player p3;
        GameBoard board;
        Card card;

        [TestInitialize]
        public void SetUp()
        {
            p1 = new HumanPlayer();
            p2 = new HumanPlayer();
            p3 = new HumanPlayer();
            card = new Thief();
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Thief(), 10);
            dict.Add(new Estate(), 10);
            dict.Add(new Copper(), 60);
            board = new GameBoard(dict);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            p2.getDeck().Push(new Copper());
            p2.getDeck().Push(new Witch());
            p3.getDeck().Push(new Gold());
            p3.getDeck().Push(new Silver());
            p1.addCardToHand(card);
        }
        //[TestMethod]
        public void TestThiefIsActionCard()
        {
            Assert.IsTrue(card.IsAction());
            Assert.IsFalse(card.IsVictory());
            Assert.IsFalse(card.IsTreasure());
        }
        //[TestMethod]
        public void TestThatThiefKeepsSomeCards()
        {
            int discardCount = p1.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 2, p1.getDiscard().Count);
        }
        //[TestMethod]
        public void TestThatThiefKeepsAllCards()
        {
            int discardCount = p1.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 3, p1.getDiscard().Count);
        }
        //[TestMethod]
        public void TestThatThiefKeepsNoCards()
        {
            int discardCount = p1.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount + 1, p1.getDiscard().Count);
        }
        //[TestMethod]
        public void TestThatThiefTrashesCards()
        {
            int deckSize2 = p2.getDeck().Count;
            int discardSize2 = p2.getDiscard().Count;
            int deckSize3 = p2.getDeck().Count;
            int discardSize3 = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(deckSize2 - 2, p2.getDeck().Count);
            Assert.AreEqual(discardSize2 + 1, p2.getDiscard().Count);
            Assert.AreEqual(deckSize3 - 2, p2.getDeck().Count);
            Assert.AreEqual(discardSize3 + 1, p2.getDiscard().Count);
        }
        //[TestMethod]
        public void TestThiefBlockedByMoat()
        {
            p2.getHand().Add(new Moat());
            p3.getHand().Add(new Moat());
            int deckSize2 = p2.getDeck().Count;
            int discardSize2 = p2.getDiscard().Count;
            int deckSize3 = p2.getDeck().Count;
            int discardSize3 = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(deckSize2, p2.getDeck().Count);
            Assert.AreEqual(discardSize2, p2.getDiscard().Count);
            Assert.AreEqual(deckSize3, p2.getDeck().Count);
            Assert.AreEqual(discardSize3, p2.getDiscard().Count);
        }
        //[TestMethod]
        public void TestThiefDiscardsIfThereAreNoTreasures()
        {
            p3.getDeck().Push(new Village());
            p3.getDeck().Push(new Market());
            int deckSize3 = p3.getDeck().Count;
            int discardSize3 = p3.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(deckSize3 - 2, p3.getDeck().Count);
            Assert.AreEqual(discardSize3 + 2, p3.getDiscard().Count);
        }


    }
}