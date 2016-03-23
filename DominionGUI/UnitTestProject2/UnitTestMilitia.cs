using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestMilitia
    {
        Player p1;
        Player p2;
        Player p3;
        GameBoard board;
        Card militia;

        [TestInitialize]
        public void SetUp()
        {
            p1 = new HumanPlayer();
            p2 = new HumanPlayer();
            p3 = new HumanPlayer();
            militia = new Militia();
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Militia(), 10);
            dict.Add(new Estate(), 10);
            dict.Add(new Copper(), 60);
            board = new GameBoard(dict);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            p1.addCardToHand(militia);
        }
        [TestMethod]
        public void TestMilitiaIsActionCard()
        {
            Assert.IsTrue(militia.IsAction());
            Assert.IsFalse(militia.IsVictory());
            Assert.IsFalse(militia.IsTreasure());
        }
        //[TestMethod]
        public void MilitiaCausesOtherPlayerDiscard()
        {
            int handcount = p2.getHand().Count;
            p1.playCard(militia);
            p2.ProcessAttacks();
            Assert.IsTrue(handcount > p2.getHand().Count);
        }
        //[TestMethod]
        public void MilitiaDoesntCauseOtherPlayerDiscardWithMoat()
        {
            p2.addCardToHand(new Moat());
            int handcount = p2.getHand().Count;
            p1.playCard(militia);
            // TODO process attack
            p2.ProcessAttacks();
            Assert.AreEqual(handcount, p2.getHand().Count);
        }
        //[TestMethod]
        public void MilitiaDoesntCausesSelfDiscard()
        {
            int handcount = p1.getHand().Count;
            p1.playCard(militia);
            // TODO process attack
            p1.ProcessAttacks();
            Assert.AreEqual(handcount - 1, p1.getHand().Count);
        }

        //[TestMethod]
        public void MilitiaDiscardsCorrectNumber()
        {
            p1.playCard(militia);
            // TODO process attack
            p2.ProcessAttacks();
            Assert.AreEqual(3, p2.getHand().Count);
        }
        //[TestMethod]
        public void MilitiaDownToThreeIfHandIsBigger()
        {
            p2.getHand().Add(p2.GetNextCard());
            p1.playCard(militia);
            // TODO process attack
            p2.ProcessAttacks();
            Assert.AreEqual(3, p2.getHand().Count);
        }
        //[TestMethod]
        public void MilitiaDiscardToThreeIfHandIsSmaller()
        {
            ArrayList list = new ArrayList();
            list.Add(new Copper());
            list.Add(new Copper());
            list.Add(new Copper());
            p2.setHand(list);
            p1.playCard(militia);
            // TODO process attack
            p2.ProcessAttacks();
            Assert.AreEqual(3, p2.getHand().Count);
        }
    }
}
