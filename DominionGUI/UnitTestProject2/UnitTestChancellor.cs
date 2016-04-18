using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.Decisions;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestChancellor
    {
        private Player player;
        private Card toPlay;
        [TestInitialize]
        public void setup()
        {
            toPlay = (new ActionCardFactory()).createNewChancellor();
        }
        [TestMethod]
        public void TestMoneyIncreases()
        {


        }
    }
}
