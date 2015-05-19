﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DominionCards;
using System.Collections;
using DominionCards.KingdomCards;

namespace UnitTestProject2
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
        public void TestSpyDiscardsPlayerTwosCard()
        {
            int discardCount = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount - 1, p2.getDiscard().Count);
        }
        [TestMethod]
        public void TestSpyDoesNotDiscardsPlayerTwosCard()
        {
            int discardCount = p2.getDiscard().Count;
            p1.playCard(card);
            Assert.AreEqual(discardCount, p2.getDiscard().Count);
        }
    }
}
