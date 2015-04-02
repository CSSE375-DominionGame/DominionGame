﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DominionCards
{
    [TestFixture()]
    class TestAddCards
    {
        [Test()]
        public void testActionCardAddedToBoard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            Queue<Player> turns = new Queue<Player>();
            GameBoard board = new GameBoard(cards, turns);
            Card newCard = new ActionCard(1, 1, 1, 1, 1);
            board.addCard(newCard);

            Assert.AreEqual(board.getCardsLeft(newCard), 12);
        }
        [Test()]
        public void testVictoryCardAddedToBoard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            Queue<Player> turns = new Queue<Player>();
            GameBoard board = new GameBoard(cards, turns);
            Card newCard = new VictoryCard(1, 1);
            board.addCard(newCard);

            Assert.AreEqual(board.getCardsLeft(newCard), 12);
        }
        [Test()]
        public void testTreasureCardAddedToBoard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            Queue<Player> turns = new Queue<Player>();
            GameBoard board = new GameBoard(cards, turns);
            Card newCard = new TreasureCard(1, 1);
            board.addCard(newCard);

            Assert.AreEqual(board.getCardsLeft(newCard), 12);
        }
    }
}
