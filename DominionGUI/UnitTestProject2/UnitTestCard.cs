using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using DominionCards;
using DominionCards.KingdomCards;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestCard
    {

        [TestMethod]
        public void TestActionCardErrorMessage()
        {
            ActionCard militia = new Militia();
            Assert.AreEqual("You must select 0 cards. Try again" ,militia.getSelectionErrorMessage());
            ActionCard mine = new Mine();
            Assert.AreEqual("You must select between 0 and 1 cards. Try again", mine.getSelectionErrorMessage());
        }

        [TestMethod]
        public void TestCardDoesNotEqualNonCard()
        {
            Assert.IsFalse(new Copper().Equals(1));
        }
        [TestMethod]
        public void TestPlayingTreasureThrowsException()
        {
            try
            {

                new Copper().Play(null);
                Assert.Fail();
            }
            catch (UnplayableCardException e)
            {
                // exception expected, test passes.
            }
        }
        [TestMethod]
        public void TestPlayingVictoryCardThrowsException()
        {
            try
            {
                new Estate().Play(null);
                Assert.Fail();
            }
            catch (UnplayableCardException e)
            {
                // exception expected, test passes.
            }
        }

        [TestMethod]
        public void testThatCardsWorkInDictionaries()
        {
            Dictionary<Card, int> dict = new Dictionary<Card, int>();
            dict.Add(new Copper(), 1);
            dict.Add(new Village(), 2);
            Assert.AreEqual(1, dict[new Copper()]);
            Assert.AreEqual(2, dict[new Village()]);
        }

        [TestMethod]
        public void TestVictoryCardsAreVictoryCards()
        {
            Assert.IsTrue(new Estate().IsVictory());
            Assert.IsTrue(new Duchy().IsVictory());
            Assert.IsTrue(new Province().IsVictory());
            Assert.IsTrue(new Gardens().IsVictory());
            Assert.IsFalse(new Estate().IsAction());
            Assert.IsFalse(new Duchy().IsAction());
            Assert.IsFalse(new Province().IsAction());
            Assert.IsFalse(new Gardens().IsAction());
            Assert.IsFalse(new Estate().IsTreasure());
            Assert.IsFalse(new Duchy().IsTreasure());
            Assert.IsFalse(new Province().IsTreasure());
            Assert.IsFalse(new Gardens().IsTreasure());
        }
        [TestMethod]
        public void TestTreasureCardsAreTresureCards()
        {
            Assert.IsFalse(new Copper().IsVictory());
            Assert.IsFalse(new Silver().IsVictory());
            Assert.IsFalse(new Gold().IsVictory());
            Assert.IsFalse(new Copper().IsAction());
            Assert.IsFalse(new Silver().IsAction());
            Assert.IsFalse(new Gold().IsAction());
            Assert.IsTrue(new Copper().IsTreasure());
            Assert.IsTrue(new Silver().IsTreasure());
            Assert.IsTrue(new Gold().IsTreasure());
        }

        [TestMethod]
        public void TestCardsPrintNames()
        {
            Assert.AreEqual("Adventurer", new Adventurer().ToString());
            Assert.AreEqual("Bureaucrat", new Bureaucrat().ToString());
            Assert.AreEqual("Cellar", new Cellar().ToString());
            Assert.AreEqual("Chancellor", new Chancellor().ToString());
            Assert.AreEqual("Chapel", new Chapel().ToString());
            Assert.AreEqual("Copper", new Copper().ToString());
            Assert.AreEqual("Council Room", new CouncilRoom().ToString());
            Assert.AreEqual("Curse", new Curse().ToString());
            Assert.AreEqual("Duchy", new Duchy().ToString());
            Assert.AreEqual("Estate", new Estate().ToString());
            Assert.AreEqual("Feast", new Feast().ToString());
            Assert.AreEqual("Festival", new Festival().ToString());
            Assert.AreEqual("Gardens", new Gardens().ToString());
            Assert.AreEqual("Gold", new Gold().ToString());
            Assert.AreEqual("Library", new Library().ToString());
            Assert.AreEqual("Laboratory", new Laboratory().ToString());
            Assert.AreEqual("Market", new Market().ToString());
            Assert.AreEqual("Militia", new Militia().ToString());
            Assert.AreEqual("Mine", new Mine().ToString());
            Assert.AreEqual("Moat", new Moat().ToString());
            Assert.AreEqual("Money Lender", new MoneyLender().ToString());
            Assert.AreEqual("Province", new Province().ToString());
            Assert.AreEqual("Remodel", new Remodel().ToString());
            Assert.AreEqual("Silver", new Silver().ToString());
            Assert.AreEqual("Smithy", new Smithy().ToString());
            Assert.AreEqual("Spy", new Spy().ToString());
            Assert.AreEqual("Thief", new Thief().ToString());
            Assert.AreEqual("Throne Room", new ThroneRoom().ToString());
            Assert.AreEqual("Village", new Village().ToString());
            Assert.AreEqual("Witch", new Witch().ToString());
            Assert.AreEqual("Woodcutter", new Woodcutter().ToString());
            Assert.AreEqual("Workshop", new Workshop().ToString());
        }
        [TestMethod]
        public void testThatAdventurerDrawsCards()
        {
            Card c = new Adventurer();
            Player p = new HumanPlayer();
            p.addCardToHand(c);
            p.drawHand();
            int count = p.getHand().Count;
            c.Play(p);
            Assert.AreEqual(count + 2, p.getHand().Count);
        }
        [TestMethod]
        public void testTreasureReturnsNoVP()
        {
            Card c = new Silver();
            Assert.AreEqual(0, c.getVictoryPoints());
        }



        //[TestMethod]
        public void testWorkshopPutsNewCardInDiscard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Estate(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Workshop();
            Player p = new HumanPlayer(1);
            board.AddPlayer(p);
            p.addCardToHand(c);
            int numdiscard = p.getDiscard().Count;
            p.playCard(c);
            Assert.AreEqual(numdiscard + 2, p.getDiscard().Count);
        }
        //[TestMethod]
        public void testWorkshopDoesntChargeForNewCard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Workshop();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int spendPoints = p1.moneyLeft();
            p1.playCard(c);
            Assert.AreEqual(spendPoints, p1.moneyLeft());
        }
        //[TestMethod]
        public void testLibrary()
        {
            Card c = new Library();
            Player p = new HumanPlayer();
            p.addCardToHand(c);
            p.drawHand();
            c.Play(p);
            Assert.AreEqual(7, p.getHand().Count);
        }
        [TestMethod]
        public void testActionReturnsNoVP()
        {
            Card c = new Village();
            Assert.AreEqual(0, c.getVictoryPoints());
        }
        // [TestMethod]
        public void testMoneyLenderAddsMoneyIfThereIsCopper()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Village());
            newHand.Add(new Copper());
            p.setHand(newHand);
            p.addCardToHand(c);
            int moneyBefore = p.moneyLeft();
            p.playCard(c);
            Assert.AreEqual(moneyBefore + 3, p.moneyLeft());
            ///////////////////////////////////
        }
        //[TestMethod]
        public void testMoneyLenderDoesNotAddMoneyIfThereIsNoCopper()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Village());
            p.setHand(newHand);
            p.addCardToHand(c);
            int moneyBefore = p.moneyLeft();
            p.playCard(c);
            Assert.AreEqual(moneyBefore, p.moneyLeft());
            ///////////////////////////////////
        }

        //[TestMethod]
        public void testMoneyLenderDoesNotAddMoneyIfNo()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Village());
            p.setHand(newHand);
            p.addCardToHand(c);
            int moneyBefore = p.moneyLeft();
            p.playCard(c);
            Assert.AreEqual(moneyBefore, p.moneyLeft());
            ///////////////////////////////////
        }
        //[TestMethod]
        public void testMoneyLenderDoesNotRemoveNoCopper()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Village());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore, p.getHand().Count);
            ///////////////////////////////////
        }
        //[TestMethod]
        public void testMoneyLenderRemovesCopper()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Village());
            newHand.Add(new Copper());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore - 1, p.getHand().Count);
            ///////////////////////////////////
        }
        //[TestMethod]
        public void testMoneyLenderOnlyRemovesOneCopper()
        {
            Card c = new MoneyLender();
            Player p = new HumanPlayer();
            List<Card> newHand = new List<Card>();
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Estate());
            newHand.Add(new Copper());
            newHand.Add(new Copper());
            p.setHand(newHand);
            int handBefore = p.getHand().Count;
            p.addCardToHand(c);
            p.playCard(c);
            Assert.AreEqual(handBefore - 1, p.getHand().Count);
            ///////////////////////////////////
        }
        [TestMethod]
        public void testVictoryReturnsVP()
        {
            Card c = new Estate();
            Assert.AreEqual(1, c.getVictoryPoints());
        }
        [TestMethod]
        public void testCardEquals()
        {
            Card copper = new Copper();
            Card village = new Village();
            Card smithy = new Smithy();

            Assert.AreNotEqual(copper, smithy);
            Assert.AreNotEqual(copper, village);
            Assert.AreNotEqual(village, smithy);
            Assert.AreNotEqual(smithy, copper);
            Assert.AreNotEqual(village, copper);
            Assert.AreNotEqual(smithy, village);

            Assert.AreEqual(copper, new Copper());
            Assert.AreEqual(village, new Village());
            Assert.AreEqual(smithy, new Smithy());
        }

        [TestMethod]
        public void testOtherPlayersDrawCardsOnCouncilRoom()
        {
            Dictionary<Card, int> cards = new Dictionary<Card,int>();
            GameBoard board = new GameBoard(cards);
            Card c = new CouncilRoom();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            int before = p2.getHand().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(before + 1, p2.getHand().Count);
        }
        //[TestMethod]
        public void TestWitchDisplaysMessage()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Curse(), 30);
            GameBoard board = new GameBoard(cards);
            Card c = new Witch();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            p1.addCardToHand(c);
            p1.playCard(c);
            p2.ProcessAttacks();
        }

        //[TestMethod]
        public void TestWitchAddsCurseToOtherPlayers()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Curse(), 30);
            GameBoard board = new GameBoard(cards);
            Card c = new Witch();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int cardsInDiscardp2 = p2.getDiscard().Count;
            int cardsInDiscardp3 = p3.getDiscard().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscardp2 + 1, p2.getDiscard().Count);
            Assert.AreEqual(cardsInDiscardp3 + 1, p3.getDiscard().Count);
            Assert.IsTrue(p2.getDiscard().Contains(new Curse()));
            Assert.IsTrue(p3.getDiscard().Contains(new Curse()));
        }
        [TestMethod]
        public void TestWitchDoesNotAddCursesIfNoCursesAreLeft()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Curse(), 0);
            GameBoard board = new GameBoard(cards);
            Card c = new Witch();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int cardsInDiscardp2 = p2.getDiscard().Count;
            int cardsInDiscardp3 = p3.getDiscard().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscardp2, p2.getDiscard().Count);
            Assert.AreEqual(cardsInDiscardp3, p3.getDiscard().Count);
            Assert.IsFalse(p2.getDiscard().Contains(new Curse()));
            Assert.IsFalse(p3.getDiscard().Contains(new Curse()));
        }

        //[TestMethod]
        public void TestWitchDoesNotAddCurseToMe()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Curse(), 30);
            GameBoard board = new GameBoard(cards);
            Card c = new Witch();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int cardsInDiscardp1 = p1.getDiscard().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscardp1 + 1, p1.getDiscard().Count);
            Assert.IsFalse(p1.getDiscard().Contains(new Curse()));
        }

        //[TestMethod]
        public void TestWitchDoesNotAddCurseToOtherPlayersWithMoats()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Curse(), 30);
            GameBoard board = new GameBoard(cards);
            Card c = new Witch();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            board.AddPlayer(p3);
            int cardsInDiscardp2 = p2.getDiscard().Count;
            int cardsInDiscardp3 = p3.getDiscard().Count;
            p2.addCardToHand(new Moat());
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscardp2, p2.getDiscard().Count);
            Assert.AreEqual(cardsInDiscardp3 + 1, p3.getDiscard().Count);
            Assert.IsFalse(p2.getDiscard().Contains(new Curse()));
            Assert.IsTrue(p3.getDiscard().Contains(new Curse()));
        }

        //[TestMethod]
        public void TestChapelDiscardThree()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Chapel();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int cardsInDiscard = p1.getDiscard().Count;
            int cardsInHand = p1.getHand().Count;
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscard + 1, p1.getDiscard().Count);
            Assert.AreEqual(cardsInHand - 4, p1.getHand().Count);
        }
        //[TestMethod]
        public void TestChapelNoCards()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Chapel();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.setHand(new List<Card>());
            p1.addCardToHand(c);
            int cardsInDiscard = p1.getDiscard().Count;
            int cardsInHand = p1.getHand().Count;
            p1.playCard(c);
            Assert.AreEqual(cardsInDiscard + 1, p1.getDiscard().Count);
            Assert.AreEqual(cardsInHand - 1, p1.getHand().Count);
        }

        //[TestMethod]
        public void testRemodelPutsNewAndPlayedCardsInDiscard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Estate(), 1);
            cards.Add(new Witch(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Remodel();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int numdiscard = p1.getDiscard().Count;
            p1.playCard(c);
            Assert.AreEqual(numdiscard + 2, p1.getDiscard().Count);
        }
        //[TestMethod]
        public void testRemodelWithNoOtherCards()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Estate(), 1);
            cards.Add(new Witch(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Remodel();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.setHand(new List<Card>());
            p1.addCardToHand(c);
            int numdiscard = p1.getDiscard().Count;
            p1.playCard(c);
            Assert.AreEqual(numdiscard + 1, p1.getDiscard().Count);
        }

        //TestMethod]
        public void testRemodelDoesntIncludeCardsMoreThanFourCost()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Witch(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Remodel();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int numdiscard = p1.getDiscard().Count;
            p1.playCard(c);
            Assert.AreEqual(numdiscard + 2, p1.getDiscard().Count);
        }

        //[TestMethod]
        public void testFeastTrashesFeastAndAddsNewToDiscard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Silver(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Feast();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int numdiscard = p1.getDiscard().Count;
            p1.playCard(c);
            Assert.AreEqual(numdiscard + 1, p1.getDiscard().Count);
        }

        //[TestMethod]
        public void testFeastDoesntIncludeCardsMoreThanFiveCost()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Copper(), 1);
            cards.Add(new Gold(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Feast();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int numdiscard = p1.getDiscard().Count;
            p1.playCard(c);
            Assert.AreEqual(numdiscard + 1, p1.getDiscard().Count);
        }

        //[TestMethod]
        public void TestMineGiveUpCopperForSilver()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Silver(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Mine();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            List<Card> newHand = new List<Card>();
            newHand.Add(new Copper());
            newHand.Add(new Copper());
            newHand.Add(new Copper());
            newHand.Add(c);
            p1.setHand(newHand);
            int cardsInHand = p1.getHand().Count;
            int moneyInHand = p1.getTotalMoney();
            p1.playCard(c);
            Assert.IsTrue(p1.getHand().Contains(new Silver()));
            Assert.AreEqual(moneyInHand + 1, p1.getTotalMoney());
            Assert.AreEqual(cardsInHand - 1, p1.getHand().Count);
        }
        //[TestMethod]
        public void TestMineWithNoTreasureCards()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Silver(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Mine();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            List<Card> newHand = new List<Card>();
            newHand.Add(c);
            p1.setHand(newHand);
            int cardsInHand = p1.getHand().Count;
            int moneyInHand = p1.getTotalMoney();
            p1.playCard(c);
            Assert.AreEqual(moneyInHand, p1.getTotalMoney());
            Assert.AreEqual(cardsInHand - 1, p1.getHand().Count);
        }

        //[TestMethod]
        public void TestCellarGainsOneActionWithNoDiscard()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Cellar();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int actions = p1.actionsLeft();
            p1.playCard(c);
            Assert.AreEqual(actions, p1.actionsLeft());
        }

        //[TestMethod]
        public void TestCellarGainsTwoActionsWithOneTrash()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Cellar();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int actions = p1.actionsLeft();
            p1.playCard(c);
            Assert.AreEqual(actions + 1, p1.actionsLeft());
        }

        //[TestMethod]
        public void TestCellarGainsThreeActionsWithOTwoTrashes()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Cellar();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int actions = p1.actionsLeft();
            p1.playCard(c);
            Assert.AreEqual(actions + 2, p1.actionsLeft());
        }

        //[TestMethod]
        public void TestChancellorDiscardsHandOnYes()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Chancellor();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int discardCount = p1.getDiscard().Count;
            int deckCount = p1.getDeck().Count;
            p1.playCard(c);
            Assert.AreEqual(discardCount + deckCount + 1, p1.getDiscard().Count);
            Assert.AreEqual(0, p1.getDeck().Count);
        }

        //[TestMethod]
        public void TestChancellorDoesntDiscardsHandOnNo()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new Chancellor();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            p1.addCardToHand(c);
            int discardCount = p1.getDiscard().Count;
            int deckCount = p1.getDeck().Count;
            p1.playCard(c);
            Assert.AreEqual(discardCount + 1, p1.getDiscard().Count);
            Assert.AreEqual(deckCount, p1.getDeck().Count);
        }

        //[TestMethod]
        public void TestMineGiveUpSilverForGold()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            cards.Add(new Gold(), 1);
            GameBoard board = new GameBoard(cards);
            Card c = new Mine();
            Player p1 = new HumanPlayer(1);
            board.AddPlayer(p1);
            List<Card> newHand = new List<Card>();
            newHand.Add(new Silver());
            newHand.Add(new Silver());
            newHand.Add(new Silver());
            newHand.Add(c);
            p1.setHand(newHand);
            int cardsInHand = p1.getHand().Count;
            int moneyInHand = p1.getTotalMoney();
            p1.playCard(c);
            Assert.IsTrue(p1.getHand().Contains(new Gold()));
            Assert.AreEqual(moneyInHand + 1, p1.getTotalMoney());
            Assert.AreEqual(cardsInHand - 1, p1.getHand().Count);
        }

        //[TestMethod]
        public void TestThroneRoomBringsUpWindow()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new ThroneRoom();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            int cardsInDiscardp2 = p2.getDiscard().Count;
            p1.addCardToHand(c);
            p1.addCardToHand(new Village());
            p1.addCardToHand(new Market());
            p1.playCard(c);
            Assert.AreEqual(2, 2);
        }
        //[TestMethod]
        public void TestThroneMakesDialogueWhenNoOtherActionCardsAvailible()
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            GameBoard board = new GameBoard(cards);
            Card c = new ThroneRoom();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            int cardsInDiscardp2 = p2.getDiscard().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(2, 2);
        }

        [TestMethod]
        public void testPlayerDraws4CardsOnCouncilRoom()
        {
            Dictionary<Card, int> cards = new Dictionary<Card,int>();
            GameBoard board = new GameBoard(cards);
            Card c = new CouncilRoom();
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            board.AddPlayer(p1);
            board.AddPlayer(p2);
            int before = p1.getHand().Count;
            p1.addCardToHand(c);
            p1.playCard(c);
            Assert.AreEqual(before + 4, p1.getHand().Count);
        }
        
    }
}
