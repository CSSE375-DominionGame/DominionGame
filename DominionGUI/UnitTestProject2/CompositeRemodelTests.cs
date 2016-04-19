using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;
using System.Collections.Generic;
using UnitTestProject.Mocks;

namespace UnitTestProject
{
    [TestClass]
    public class CompositeRemodelTests
    {
        private Player p;
        private GameBoard gb;
        private Card c;
        private Card cardGotten;

        [TestInitialize]
        public void Setup()
        {
            cardGotten = new Silver();
            List<Card> cardSelectedToTrash = new List<Card>();
            cardSelectedToTrash.Add(new Estate());

            List<Card> cardSelectedToBuy = new List<Card>();
            cardSelectedToBuy.Add(cardGotten);

            setupGameBoard();

            Queue<List<Card>> q = new Queue<List<Card>>();
            q.Enqueue(cardSelectedToTrash);
            q.Enqueue(cardSelectedToBuy);

            p = new PlayerCompositeMock(q);
            c = (new ActionCardFactory()).createNewRemodel();
            p.getHand().Add(c);
        }

        private void setupGameBoard()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict[cardGotten] = 10;
            gb = new GameBoard(dict);
        }


        [TestMethod]
        public void TrashDecisionReducesHand()
        {
            int handSize = p.getHand().Count;
            p.playCard(c);
            Assert.AreEqual(handSize - 2, p.getHand().Count);
        }
        [TestMethod]
        public void TrashDecisionDoesIncreaseDiscard()
        {
            int discard = p.getDiscard().Count;
            p.playCard(c);
            Assert.AreEqual(discard + 2, p.getDiscard().Count);
        }
        [TestMethod]
        public void TrashDecisionDiscardsRemodel()
        {
            p.playCard(c);
            Assert.IsTrue(p.getDiscard().Contains(c));
        }
        [TestMethod]
        public void TrashDecisionDoesDiscardsSilver()
        {
            p.playCard(c);
            Assert.IsTrue(p.getDiscard().Contains(cardGotten));
        }
        [TestMethod]
        public void TrashDecisionDoesNotDiscardEstate()
        {
            p.playCard(c);
            Assert.IsFalse(p.getDiscard().Contains(new Estate()));
        }
        [TestMethod]
        public void TrashDecisionDoesIncreaseDeck()
        {
            int deck = p.getDeck().Count;
            p.playCard(c);
            Assert.AreEqual(deck, p.getDeck().Count);
        }
        [TestMethod]
        public void RemovesCardFromGameBoard()
        {
            int cardsLeft = gb.getCardsLeft(cardGotten);
            p.playCard(c);
            Assert.AreEqual(cardsLeft - 1, gb.getCardsLeft(cardGotten));
        }
    }
}
