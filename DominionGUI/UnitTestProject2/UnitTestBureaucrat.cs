﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestBureaucrat
    {
        GameBoard board;
        Player p1;
        Player p2;
        Player p3;
        Card card;

        [TestInitialize]
        public void SetUp()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            board = new GameBoard(dict);
            p1 = new HumanPlayer(1);
            p1.setHand(new ArrayList());
            p2 = new HumanPlayer(2);
            p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            card = new Bureaucrat();
            p1.addCardToHand(card);
        }

        [TestMethod]
        public void TestNextCardIsSilverAfterPlayingBeaureauqwertyuiop()
        {
            p1.playCard(card);
            ProcessAllAttacks();
            Assert.AreEqual(new Silver(), p1.GetNextCard());
        }
        [TestMethod]
        public void TestNextCardPlayingBeaureauqwertyuiopDoesNothingToPlayerWithNoVictoryCards()
        {
            p2.setDeck(new Stack<Card>());
            p2.addCardToHand(new Copper());
            p3.setDeck(new Stack<Card>());
            p3.addCardToHand(new Copper());
            p1.playCard(card);
            ProcessAllAttacks();

            int cardID2 = p2.GetNextCard().getID();
            int cardID3 = p3.GetNextCard().getID();
            Assert.AreNotEqual(3, cardID2);
            Assert.AreNotEqual(4, cardID2);
            Assert.AreNotEqual(5, cardID2);
            Assert.AreNotEqual(3, cardID3);
            Assert.AreNotEqual(4, cardID3);
            Assert.AreNotEqual(5, cardID3);
        }
        [TestMethod]
        public void TestNextCardPlayingBeaureauqwertyuiopRemovesVictoryCardFromHand()
        {
            p2.getHand().Clear();
            p3.getHand().Clear();
            p2.addCardToHand(new Estate());
            p3.addCardToHand(new Estate());
            p2.addCardToHand(new Copper());
            p3.addCardToHand(new Copper());

            p1.playCard(card);
            ProcessAllAttacks();

            Assert.IsFalse(p2.getHand().Contains(new Estate()));
            Assert.IsFalse(p3.getHand().Contains(new Estate()));

        }
        [TestMethod]
        public void TestNextCardPlayingBeaureauqwertyuiopAddsVictoryCardToDeck()
        {
            p2.addCardToHand(new Estate());
            p3.addCardToHand(new Estate());
            p1.playCard(card);
            ProcessAllAttacks();
            Assert.AreEqual(new Estate(), p2.GetNextCard());
            Assert.AreEqual(new Estate(), p3.GetNextCard());
        }

        private void ProcessAllAttacks()
        {
            p1.ProcessAttacks();
            p2.ProcessAttacks();
            p3.ProcessAttacks();
        }
    }
}
