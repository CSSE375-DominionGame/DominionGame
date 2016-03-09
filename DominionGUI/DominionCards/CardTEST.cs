using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    [TestClass]
    class CardTEST
    {
        /*
        [Test()]
        public void testThatCardsWorkInDictionaries()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new KingdomCards.Copper(), 1);
            dict.Add(new KingdomCards.Village(), 2);
            Assert.AreEqual(1, dict[new KingdomCards.Copper()]);
            Assert.AreEqual(2, dict[new KingdomCards.Village()]);
        }
        [Test()]
        public void testThatAdventurerDoesThing()
        {
            Card c = new KingdomCards.Adventurer();
            Player p = new HumanPlayer();
            p.addCardToHand(c);
            p.drawHand();
            int count = p.getHand().Count;
            c.play(p);
            Assert.AreEqual(count + 2, p.getHand().Count);
        }
        [Test()]
        public void testTreasureReturnsNoVP()
        {
            Card c = new KingdomCards.Silver();
            Assert.AreEqual(0, c.getVictoryPoints());
        }
        [Test()]
        public void testLibrary()
        {
            Card c = new KingdomCards.Library();
            Player p = new HumanPlayer();
            p.addCardToHand(c);
            p.drawHand();
            c.play(p);
            Assert.AreEqual(7, p.getHand().Count);
        }
        [Test()]
        public void testActionReturnsNoVP()
        {
            Card c = new KingdomCards.Village();
            Assert.AreEqual(0, c.getVictoryPoints());
        }
        [Test()]
        public void testMoneyLenderAddsMoneyIfThereIsCopper()
        {
            Card c = new KingdomCards.MoneyLender();
            Player p = new HumanPlayer();
            ArrayList newHand = new ArrayList();
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Village());
            newHand.Add(new KingdomCards.Copper());
            p.setHand(newHand);
            p.addCardToHand(c);
            int moneyBefore = p.moneyLeft();
            p.playCard(c);
            Assert.AreEqual(moneyBefore + 3, p.moneyLeft());
            ///////////////////////////////////
        }
        [Test()]
        public void testMoneyLenderDoesNotAddMoneyIfThereIsNoCopper()
        {
            Card c = new KingdomCards.MoneyLender();
            Player p = new HumanPlayer();
            ArrayList newHand = new ArrayList();
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Village());
            p.setHand(newHand);
            p.addCardToHand(c);
            int moneyBefore = p.moneyLeft();
            p.playCard(c);
            Assert.AreEqual(moneyBefore, p.moneyLeft());
            ///////////////////////////////////
        }
        [Test()]
        public void testMoneyLenderDoesNotRemoveNoCopper()
        {
            Card c = new KingdomCards.MoneyLender();
            Player p = new HumanPlayer();
            ArrayList newHand = new ArrayList();
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Village());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore, p.getHand().Count);
            ///////////////////////////////////
        }
        [Test()]
        public void testMoneyLenderRemovesCopper()
        {
            Card c = new KingdomCards.MoneyLender();
            Player p = new HumanPlayer();
            ArrayList newHand = new ArrayList();
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Village());
            newHand.Add(new KingdomCards.Copper());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore-1, p.getHand().Count);
            ///////////////////////////////////
        }
        [Test()]
        public void testMoneyLenderOnlyRemovesOneCopper()
        {
            Card c = new KingdomCards.MoneyLender();
            Player p = new HumanPlayer();
            ArrayList newHand = new ArrayList();
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Estate());
            newHand.Add(new KingdomCards.Copper());
            newHand.Add(new KingdomCards.Copper());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore-1, p.getHand().Count);
            ///////////////////////////////////
        }
        [Test()]
        public void testVictoryReturnsVP()
        {
            Card c = new KingdomCards.Estate();
            Assert.AreEqual(1, c.getVictoryPoints());
        }
        [Test()]
        public void testCardEquals()
        {
            Card copper = new KingdomCards.Copper();
            Card village = new KingdomCards.Village();
            Card smithy = new KingdomCards.Smithy();
            
            Assert.AreNotEqual(copper, smithy);
            Assert.AreNotEqual(copper, village);
            Assert.AreNotEqual(village, smithy);
            Assert.AreNotEqual(smithy, copper);
            Assert.AreNotEqual(village, copper);
            Assert.AreNotEqual(smithy, village);

            Assert.AreEqual(copper, new KingdomCards.Copper());
            Assert.AreEqual(village, new KingdomCards.Village());
            Assert.AreEqual(smithy, new KingdomCards.Smithy());
        }

        /*[Test()]
        public void testGetPriceVictoryCard()
        {
            int vPrice = 2;
            Card v = new VictoryCard(1, vPrice, Card.GENERIC_CARD_ID);
            Assert.AreEqual(v.getPrice(), vPrice);
        }
        [Test()]
        public void testGetPriceTreasureCard()
        {
            int tPrice = 2;
            Card t = new TreasureCard(1, tPrice, Card.GENERIC_CARD_ID);
            Assert.AreEqual(t.getPrice(), tPrice);
        }
        [Test()]
        public void testGetPriceActionCard()
        {
            int aPrice = 2;
            Card a = new ActionCard(1, 1, 1, 1, aPrice, Card.GENERIC_CARD_ID);
            Assert.AreEqual(a.getPrice(), aPrice);
        }*/
    }
}
