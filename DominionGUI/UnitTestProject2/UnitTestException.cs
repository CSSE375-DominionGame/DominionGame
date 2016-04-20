using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestException
    {
        GamePhaseEnd endPhase;

        [TestInitialize]
        public void setupGameTest()
        {
            endPhase = new GamePhaseEnd();
        }
        [TestMethod]
        public void TieExceptionReturnsCorrectStringForTwoPlayers()
        {
            Player p1 = new HumanPlayer(1);
            Player p3 = new HumanPlayer(3);
            TieException exc = new TieException(p1, p3, 9, 10);
            String expected = "Player 1, and Player 3 tied for winner! \n";
            Assert.AreEqual(expected, exc.PrintWinners());
        }

        [TestMethod]
        public void BreakTieReturnsTrueIfNewPlayerWins()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p3.getDiscard().Add(new Province());
            endPhase.setEndPhase(p1);
            TieException exception = new TieException(p1, p2, endPhase.countVictoryPoints(), endPhase.getTotalMoney());
            Assert.IsTrue(exception.BreaksTie(p3));
        }
        [TestMethod]
        public void BreakTieReturnsFalseIfNewPlayerLoses()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p1.getDiscard().Add(new Province());
            p2.getDiscard().Add(new Province());
            endPhase.setEndPhase(p1);
            TieException exception = new TieException(p1, p2, endPhase.countVictoryPoints(), endPhase.getTotalMoney());
            Assert.IsFalse(exception.BreaksTie(p3));
        }

        [TestMethod]
        public void BreakTieDoesNotAddPlayerToTieIfNewPlayerLoses()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p1.getDiscard().Add(new Province());
            p2.getDiscard().Add(new Province());
            endPhase.setEndPhase(p1);
            TieException exception = new TieException(p1, p2, endPhase.countVictoryPoints(), endPhase.getTotalMoney());
            int arraySize = exception.getArraySize();
            exception.BreaksTie(p3);
            Assert.AreEqual(arraySize, exception.getArraySize());
        }
    }
}
