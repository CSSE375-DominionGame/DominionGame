﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;
using System.Collections.Generic;
//using Rhino.Mocks;
//using Rhino.Mocks.Interfaces;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestGameBoard
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
        public void TestTurnOrderDoesNotChange()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);

            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            Player p2 = new HumanPlayer(1);
            board.AddPlayer(p2);
            Player p3 = new HumanPlayer(1);
            board.AddPlayer(p3);
            Player p4 = new HumanPlayer(1);
            board.AddPlayer(p4);

            Assert.AreSame(p1, board.NextPlayer());
            Assert.AreSame(p2, board.NextPlayer());
            Assert.AreSame(p3, board.NextPlayer());
            Assert.AreSame(p4, board.NextPlayer());
        }
        [TestMethod]
        public void TestAddPlayerAddsAPlayer()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            Assert.AreSame(p1, board.NextPlayer());
        }
        [TestMethod]
        public void TestAddPlayerReturnsTrueWhenPlayerAdded()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            Assert.IsTrue(board.AddPlayer(p1));
        }
        [TestMethod]
        public void TestAddPlayerReturnsFalseWhenPlayerAlreadyInQueue()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            Assert.IsFalse(board.AddPlayer(p1));
        }

        [TestMethod]
        public void TestGameEndsTrueWhenProvincesRunOut()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            cards[new Province()] = 0;
            Assert.IsTrue(board.GameIsOver());
        }
        [TestMethod]
        public void TestGameEndsTrueWhenThreePilesRunOut()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            cards[new Cellar()] = 0;
            cards[new Moat()] = 0;
            cards[new Woodcutter()] = 0;
            Assert.IsTrue(board.GameIsOver());
        }
        [TestMethod]
        public void TestWhenGameBoardInstanceIsNull()
        {
            try
            {
                GameBoard.getInstance();
                Assert.Fail();
            }
            catch (GameBoardInstanceIsNullException e)
            {
                // testing if exception is thrown. Test passed.
            }
        }

        [TestMethod]
        public void TestGameEndsFalseIfOnlyTwoStacksEmpty()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            cards[new Cellar()] = 0;
            cards[new Estate()] = 0;
            Assert.IsFalse(board.GameIsOver());
        }
        [TestMethod]
        public void TestThatGetInstanceRetutrnsCorrectBoard()
        {
            GameBoard board = new GameBoard(GetTestCards());
            Assert.AreSame(board, GameBoard.getInstance());
        }
        
        [TestMethod]
        public void IntegrationTestTieIsThrownOnTrueTie()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            Player p1 = new SpecialPlayerMock(1);
            Player p2 = new SpecialPlayerMock(2);
            cards[new Province()] = 8;
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            try
            {
                board.PlayGame();
            }
            catch (TieException e)
            {
                Assert.AreEqual(2, e.getArraySize());
                return;
            }
            Assert.Fail("expected an exception");

        }
        [TestMethod]
        public void IntegrationTestPlayerWinsWithMoreVP()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            Player p1 = new SpecialPlayerMock(1);
            Player p2 = new SpecialPlayerMock(2);

            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.GetCards()[new Province()] = 8;
            p1.getDiscard().Add(new Province());
            try
            {
                Assert.AreSame(p1, board.PlayGame());
            }
            catch (TieException e)
            {
                Assert.Fail("Tie should not have occured");
            }
        }
        [TestMethod]
        public void IntegrationTestTieBrokenByMoney()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            Player p1 = new SpecialPlayerMock(1);
            Player p2 = new SpecialPlayerMock(2);

            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.GetCards()[new Province()] = 8;
            p1.getDiscard().Add(new Gold());
            try
            {
                Assert.AreSame(p1, board.PlayGame());
            }
            catch (TieException e)
            {
                Assert.Fail("Tie should not have occured");
            }
        }
        [TestMethod]
        public void IntegrationTestMoreGoldDoesNotWinWithLessVP()
        {
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            Player p1 = new SpecialPlayerMock(1);
            Player p2 = new SpecialPlayerMock(2);

            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.GetCards()[new Province()] = 8;
            p1.getDiscard().Add(new Gold());
            p2.getDiscard().Add(new Province());
            try
            {
                Assert.AreSame(p2, board.PlayGame());
            }
            catch (TieException e)
            {
                Assert.Fail("Tie should not have occured");
            }
        }
        [TestMethod]
        public void IntegrationTestTieIsThrownOnTrueTieWithFourPlayers()
        {
            //            MockRepository mocks = new MockRepository();
            Dictionary<Card, int> cards = GetTestCards();
            GameBoard board = new GameBoard(cards);
            Player p1 = new SpecialPlayerMock(1);
            Player p2 = new SpecialPlayerMock(2);
            Player p3 = new SpecialPlayerMock(3);
            Player p4 = new SpecialPlayerMock(4);

            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            board.AddPlayer(p4);
            board.GetCards()[new Province()] = 12;
            try
            {
                board.PlayGame();
                Assert.Fail("expected an exception");
            }
            catch (TieException e)
            {
                Assert.AreEqual(4, e.getArraySize());
            }
        }
        public class SpecialPlayerMock : HumanPlayer
        {
            public int numbTimesCalled;
            
            public SpecialPlayerMock(int n) : base(n)
            {
                numbTimesCalled = 0;
            }
            public override void TakeTurn()
            {
                numbTimesCalled++;
                GameBoard.getInstance().GetCards()[new Province()] -= 1;
                getDiscard().Add(new Province());
            }
        }
    }
}
