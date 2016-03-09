using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionGUI
{
    [TestClass]
    public class TestAddRandomCards
    {
        [TestMethod]
        public void TestIfCorrectNumberOfCardsAreAddedToGameBoard()
        {
            new SelectNumPlayers();
            for (int i = 0; i < 100; i++)
            {
                SelectNumPlayers.INSTANCE.addRandomCards();
                Assert.AreEqual(10, SelectNumPlayers.INSTANCE.board.cards.Count);
            }
        }
    }
}
