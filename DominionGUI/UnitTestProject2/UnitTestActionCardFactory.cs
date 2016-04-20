using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestActionCardFactory
    {
        private ActionCardFactory factory = new ActionCardFactory();
        private Player player;

        [TestMethod]
        public void TestcreateAdventurer()
        {
            Assert.AreEqual(factory.createNewAdventurer(), new Adventurer());
        }

        [TestMethod]
        public void TestcreateCellar()
        {
            ActionCard cellar = factory.createNewCellar();
            Assert.AreEqual(2, cellar.getPrice());
            Assert.AreEqual(0, cellar.getVictoryPoints());
            Assert.AreEqual(1, cellar.actions);
            Assert.AreEqual(0, cellar.buys);
        }

        [TestMethod]
        public void TestcreateChancellor()
        {
            ActionCard chancellor = factory.createNewChancellor();
            Assert.AreEqual(3, chancellor.getPrice());
            Assert.AreEqual(0, chancellor.getVictoryPoints());
            Assert.AreEqual(0, chancellor.actions);
            Assert.AreEqual(0, chancellor.buys);
        }

        [TestMethod]
        public void TestcreateChapel()
        {
            ActionCard chapel = factory.createNewChapel();
            Assert.AreEqual(2, chapel.getPrice());
            Assert.AreEqual(0, chapel.getVictoryPoints());
            Assert.AreEqual(0, chapel.actions);
            Assert.AreEqual(0, chapel.buys);
        }

        [TestMethod]
        public void TestcreateCouncilRoom()
        {
            Assert.AreEqual(factory.createNewCouncilRoom(), new CouncilRoom());
        }

        [TestMethod]
        public void TestcreateFeast()
        {
            ActionCard Feast = factory.createNewFeast();
            Assert.AreEqual(4, Feast.getPrice());
            Assert.AreEqual(0, Feast.getVictoryPoints());
            Assert.AreEqual(0, Feast.actions);
            Assert.AreEqual(0, Feast.buys);
        }

        [TestMethod]
        public void TestcreateFestival()
        {
            Card festival = factory.createNewFestival();
            player = new HumanPlayer();
            Assert.AreEqual(5, festival.getPrice());
            Assert.AreEqual(0, festival.getVictoryPoints());
            player.addCardToHand(festival);
            player.playCard(festival);
            Assert.AreEqual(2, player.actionsLeft());
            Assert.AreEqual(2, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateLaboratory()
        {
            Card laboratory = factory.createNewLaboratory();
            player = new HumanPlayer();
            Assert.AreEqual(5, laboratory.getPrice());
            Assert.AreEqual(0, laboratory.getVictoryPoints());
            player.addCardToHand(laboratory);
            player.playCard(laboratory);
            Assert.AreEqual(1, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(7, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateLibrary()
        {
            Assert.AreEqual(factory.createNewLibrary(), new Library());
        }

        [TestMethod]
        public void TestcreateMarket()
        {
            ActionCard market = factory.createNewMarket();
            Assert.AreEqual(5, market.getPrice());
            Assert.AreEqual(0, market.getVictoryPoints());
            Assert.AreEqual(1, market.actions);
            Assert.AreEqual(1, market.buys);
        }

        [TestMethod]
        public void TestcreateMine()
        {
            ActionCard mine = factory.createNewMine();
            Assert.AreEqual(5, mine.getPrice());
            Assert.AreEqual(0, mine.getVictoryPoints());
            Assert.AreEqual(0, mine.actions);
            Assert.AreEqual(0, mine.buys);
        }


        [TestMethod]
        public void TestcreateMoneyLender()
        {
            Assert.AreEqual(factory.createNewMoneyLender(), new MoneyLender());
        }

        [TestMethod]
        public void TestcreateRemodel()
        {
            ActionCard remodel = factory.createNewRemodel();
            Assert.AreEqual(4, remodel.getPrice());
            Assert.AreEqual(0, remodel.getVictoryPoints());
            Assert.AreEqual(0, remodel.actions);
            Assert.AreEqual(0, remodel.buys);
        }

        [TestMethod]
        public void TestcreateSmithy()
        {
            ActionCard smithy = factory.createNewSmithy();
            Assert.AreEqual(4, smithy.getPrice());
            Assert.AreEqual(0, smithy.getVictoryPoints());
            Assert.AreEqual(0, smithy.actions);
            Assert.AreEqual(0, smithy.buys);
        }

        [TestMethod]
        public void TestcreateThroneRoom()
        {
            ActionCard throneRoom = factory.createNewThroneRoom();
            Assert.AreEqual(4, throneRoom.getPrice());
            Assert.AreEqual(0, throneRoom.getVictoryPoints());
            Assert.AreEqual(0, throneRoom.actions);
            Assert.AreEqual(0, throneRoom.buys);
        }

        [TestMethod]
        public void TestcreateVillage()
        {
            ActionCard village = factory.createNewVillage();
            Assert.AreEqual(3, village.getPrice());
            Assert.AreEqual(0, village.getVictoryPoints());
            Assert.AreEqual(2, village.actions);
            Assert.AreEqual(0, village.buys);
        }

        [TestMethod]
        public void TestcreateWoodcutter()
        {
            ActionCard woodcutter = factory.createNewWoodcutter();
            Assert.AreEqual(3, woodcutter.getPrice());
            Assert.AreEqual(0, woodcutter.getVictoryPoints());
            Assert.AreEqual(0, woodcutter.actions);
            Assert.AreEqual(1, woodcutter.buys);
        }

        [TestMethod]
        public void TestcreateWorkshop()
        {
            ActionCard workshop = factory.createNewWorkshop();
            player = new DumbAiPlayer(0);
            Assert.AreEqual(3, workshop.getPrice());
            Assert.AreEqual(0, workshop.getVictoryPoints());
            Assert.AreEqual(0, workshop.actions);
            Assert.AreEqual(0, workshop.buys);
        }
    }
}
