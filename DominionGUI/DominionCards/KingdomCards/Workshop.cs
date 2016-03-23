using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Workshop : ActionCard
    {
        private static int ID = 31;
        public Workshop()
            : base(0, 0, 0, 0, 3, ID)
        {
            // Uses ActionCard Constructor
        }
        public override void Play(Player player)
        {
            List<Card> buyableCards = new List<Card>();
            foreach (Card card in GameBoard.getInstance().cards.Keys)
            {
                int cost = card.getPrice();
                if (cost < 5)
                {
                    buyableCards.Add(card);
                }
            }
            List<Card> cards = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
            while (cards.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to gain.  Try again");
                cards = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
            }
            Card cardSelected = (Card)cards[0];

            player.getDiscard().Add(cardSelected);
            GameBoard.getInstance().GetCards()[cardSelected] -= 1;
        }
        public override String ToString()
        {
            return "Workshop";
        }
    }
}
