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
            Card cellar = factory.createNewCellar();
            player = new HumanPlayer();
            Assert.AreEqual(2, cellar.getPrice());
            Assert.AreEqual(0, cellar.getVictoryPoints());
            player.addCardToHand(cellar);
            player.playCard(cellar);
            Assert.AreEqual(1, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateChancellor()
        {
            Card chancellor = factory.createNewChancellor();
            player = new HumanPlayer();
            Assert.AreEqual(3, chancellor.getPrice());
            Assert.AreEqual(0, chancellor.getVictoryPoints());
            player.addCardToHand(chancellor);
            player.playCard(chancellor);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateChapel()
        {
            Card chapel = factory.createNewChapel();
            player = new HumanPlayer();
            Assert.AreEqual(2, chapel.getPrice());
            Assert.AreEqual(0, chapel.getVictoryPoints());
            player.addCardToHand(chapel);
            player.playCard(chapel);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateCouncilRoom()
        {
            Assert.AreEqual(factory.createNewCouncilRoom(), new CouncilRoom());
        }

        [TestMethod]
        public void TestcreateFeast()
        {
            Card Feast = factory.createNewFeast();
            player = new HumanPlayer();
            Assert.AreEqual(4, Feast.getPrice());
            Assert.AreEqual(0, Feast.getVictoryPoints());
            player.addCardToHand(Feast);
            player.playCard(Feast);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
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
            Card market = factory.createNewMarket();
            player = new HumanPlayer();
            Assert.AreEqual(5, market.getPrice());
            Assert.AreEqual(0, market.getVictoryPoints());
            player.addCardToHand(market);
            player.playCard(market);
            Assert.AreEqual(1, player.actionsLeft());
            Assert.AreEqual(2, player.buysLeft());
            Assert.AreEqual(6, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateMine()
        {
            Card mine = factory.createNewMine();
            player = new HumanPlayer();
            Assert.AreEqual(5, mine.getPrice());
            Assert.AreEqual(0, mine.getVictoryPoints());
            player.addCardToHand(mine);
            player.playCard(mine);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }


        [TestMethod]
        public void TestcreateMoneyLender()
        {
            Assert.AreEqual(factory.createNewMoneyLender(), new MoneyLender());
        }

        [TestMethod]
        public void TestcreateRemodel()
        {
            Card remodel = factory.createNewRemodel();
            player = new HumanPlayer();
            Assert.AreEqual(4, remodel.getPrice());
            Assert.AreEqual(0, remodel.getVictoryPoints());
            player.addCardToHand(remodel);
            player.playCard(remodel);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateSmithy()
        {
            Card smithy = factory.createNewSmithy();
            player = new HumanPlayer();
            Assert.AreEqual(4, smithy.getPrice());
            Assert.AreEqual(0, smithy.getVictoryPoints());
            player.addCardToHand(smithy);
            player.playCard(smithy);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(8, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateThroneRoom()
        {
            Card throneRoom = factory.createNewThroneRoom();
            player = new HumanPlayer();
            Assert.AreEqual(4, throneRoom.getPrice());
            Assert.AreEqual(0, throneRoom.getVictoryPoints());
            player.addCardToHand(throneRoom);
            player.playCard(throneRoom);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateVillage()
        {
            Card village = factory.createNewVillage();
            player = new HumanPlayer();
            Assert.AreEqual(3, village.getPrice());
            Assert.AreEqual(0, village.getVictoryPoints());
            player.addCardToHand(village);
            player.playCard(village);
            Assert.AreEqual(2, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(6, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateWoodcutter()
        {
            Card woodcutter = factory.createNewWoodcutter();
            player = new HumanPlayer();
            Assert.AreEqual(3, woodcutter.getPrice());
            Assert.AreEqual(0, woodcutter.getVictoryPoints());
            player.addCardToHand(woodcutter);
            player.playCard(woodcutter);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(2, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }

        [TestMethod]
        public void TestcreateWorkshop()
        {
            Card workshop = factory.createNewWorkshop();
            player = new HumanPlayer();
            Assert.AreEqual(3, workshop.getPrice());
            Assert.AreEqual(0, workshop.getVictoryPoints());
            player.addCardToHand(workshop);
            player.playCard(workshop);
            Assert.AreEqual(0, player.actionsLeft());
            Assert.AreEqual(1, player.buysLeft());
            Assert.AreEqual(5, player.getHand().Count);
        }
    }
}
