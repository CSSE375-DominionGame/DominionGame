using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for UnitTest3
    /// </summary>
    [TestClass]
    public class AIPlayerTest
    {

        private Player ai, attacker;
        private GameBoard gb;
        private Card card1, atkCard;
        private ActionCardFactory fact;

        [TestInitialize]
        public void Setup()
        {
            ai = new AiPlayer(1);
            attacker = new HumanPlayer(2);
            Dictionary<Card, int> cards = new Dictionary<Card,int>();
            fact = new ActionCardFactory();
            cards.Add(fact.createNewWoodcutter(), 10);
            cards.Add(fact.createNewCopper(), 10);
            cards.Add(fact.createNewMoat(), 10);
            cards.Add(fact.createNewMilitia(), 10);
            cards.Add(fact.createNewMarket(), 10);
            cards.Add(fact.createNewAdventurer(), 10);

            gb = new GameBoard(cards);
            gb.AddPlayer(attacker); // this order is important for enqueue attacks to function correctly in militia
            gb.AddPlayer(ai);

            card1 = fact.createNewWorkshop();
            ai.getHand().Add(card1);

            atkCard = fact.createNewMilitia();
            attacker.getHand().Add(atkCard);
        }

        [TestMethod]
        public void PlayingWorkshopDiscardsPlayedCardAndGainedCard()
        {
            int expected = ai.getDiscard().Count + 2;
            ai.playCard(card1);
            Assert.AreEqual(expected, ai.getDiscard().Count);
        }
        [TestMethod]
        public void PlayingWorkshopReducesMilitiaInGameBoard()
        {
            int expected = gb.getCardsLeft(fact.createNewWoodcutter()) - 1;
            ai.playCard(card1);
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewWoodcutter()));
        }
        [TestMethod]
        public void PlayingWorkshopDoesNotReduceOtherCards()
        {
            int expected = gb.getCardsLeft(fact.createNewMarket());
            ai.playCard(card1);
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewCopper()));
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewMoat()));
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewMilitia()));
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewMarket()));
            Assert.AreEqual(expected, gb.getCardsLeft(fact.createNewAdventurer()));
        }

        [TestMethod]
        public void MilitiaCausesCardsDiscard()
        {
            int initialDiscard = ai.getDiscard().Count;
            attacker.playCard(atkCard);
            ai.ProcessAttacks();
            Assert.IsTrue(initialDiscard < ai.getDiscard().Count);
        }

        [TestMethod]
        public void MilitiaCausesHandToShrink()
        {
            int hand = ai.getHand().Count;
            attacker.playCard(atkCard);
            ai.ProcessAttacks();
            Assert.IsTrue(hand > ai.getHand().Count);
        }

        [TestMethod]
        public void MilitiaSetsHandToThree()
        {
            int expected = 3;
            attacker.playCard(atkCard);
            ai.ProcessAttacks();
            Assert.AreEqual(expected, ai.getHand().Count);
        }
        
        //[TestMethod]
        public void BuysCardOfMaxCost4()
        {
            
        }

        //[TestMethod]
        public void BuysCardOfMaxCost5()
        {

        }
    }
}
