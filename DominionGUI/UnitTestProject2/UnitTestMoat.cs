using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestMoat
    {
        GameBoard board;
        Player p1, p2, p3;
        Card card;

        [TestInitialize]
        public void SetUp()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Curse(), 10);
            board = new GameBoard(dict);
            p1 = new HumanPlayer(1);
            p1.setHand(new ArrayList());
            p2 = new HumanPlayer(2);
            p2.setHand(new ArrayList());
            p3 = new HumanPlayer(3);
            p3.setHand(new ArrayList());
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            card = new Moat();
            p1.addCardToHand(card);
        }

        [TestMethod]
        public void PlayingMoatDoesNotTreatAsAttack()
        {
            int p2attacks = p2.getAttacks().Count;
            int p3attacks = p3.getAttacks().Count;

            p1.playCard(card);
            Assert.AreEqual(p2attacks, p2.getAttacks().Count);
            Assert.AreEqual(p3attacks, p3.getAttacks().Count);
        }
        // [TestMethod]
        public void TestMoatEnquedInsteadOfBlockedAttack()
        {
            p2.addCardToHand(new Moat());
            p1.addCardToHand(new Witch());
            p1.playCard(new Witch());

            Assert.AreEqual(new Moat(), p2.getAttacks().Dequeue());
            Assert.AreEqual(new Witch(), p3.getAttacks().Dequeue());
            Assert.AreEqual(0, p1.getAttacks().Count);
        }
    }
}
