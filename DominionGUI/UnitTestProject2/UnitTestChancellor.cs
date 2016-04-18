using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.Decisions;
using DominionCards.KingdomCards;
using UnitTestProject.Mocks;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestChancellor
    {
        private Player noMan, yesMan;
        private Card toPlay;
        [TestInitialize]
        public void Setup()
        {
            toPlay = (new ActionCardFactory()).createNewChancellor();
            noMan = new PlayerMockSelectNo();
            noMan.getHand().Add(toPlay);
            yesMan = new PlayerMockSelectYes();
            yesMan.getHand().Add(toPlay);
        }
        
        [TestMethod]
        public void MoneyIncreasesYes()
        {
            int startingMoney = yesMan.money;
            yesMan.playCard(toPlay);
            int moneyAfter = yesMan.money;
            Assert.AreEqual(startingMoney + 2, moneyAfter);
        }

        [TestMethod]
        public void MoneyNotIncreasesNo()
        {
            int startingMoney = noMan.money;
            noMan.playCard(toPlay);
            int moneyAfter = noMan.money;
            Assert.AreEqual(startingMoney + 2, moneyAfter);
        }

        [TestMethod]
        public void DeckRemovedYes()
        {
            yesMan.playCard(toPlay);
            Assert.AreEqual(0, yesMan.getDeck().Count);
        }

        [TestMethod]
        public void DeckNotRemovedNo()
        {
            int deckSize = noMan.getDeck().Count;
            noMan.playCard(toPlay);
            Assert.AreEqual(deckSize, noMan.getDeck().Count);
        }

        [TestMethod]
        public void DiscardIncreasedYes()
        {
            int discardSize = yesMan.getDiscard().Count;
            int deckSize = yesMan.getDeck().Count;
            yesMan.playCard(toPlay);
            Assert.AreEqual(discardSize + deckSize + 1, yesMan.getDiscard().Count);
        }

        [TestMethod]
        public void DiscardNotIncreasedNo()
        {
            int discardSize = noMan.getDiscard().Count;
            noMan.playCard(toPlay);
            Assert.AreEqual(discardSize + 1, noMan.getDiscard().Count);
        }

        // [TestMethod]
        public void Test()
        {

        }
    }
}
