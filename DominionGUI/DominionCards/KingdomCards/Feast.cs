using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Feast : ActionCard
    {
        private static int ID = 12;
        public Feast()
            : base("Feast", 0, 0, 0, 0, 4, ID)
        {
            // Uses ActionCard Constructor
        }
        public override String ToString()
        {
            return "Feast";
        }

        public override void Play(Player player)
        {
            //trash the feast
            player.getDiscard().Remove(new Feast());
            //pick a card to gain
            List<Card> buyableCards = new List<Card>();
            foreach (Card card in GameBoard.getInstance().cards.Keys)
            {
                int cost = card.getPrice();
                if (cost < 6)
                {
                    buyableCards.Add(card);
                }
            }
            //List<Card> cards = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
            List<Card> cards = player.SelectCards(this.decision);
            while (cards.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to gain.  Try again");
                //cards = player.SelectCards(buyableCards, "Choose a card to gain.", 1);
                cards = player.SelectCards(this.decision);
            }
            Card cardSelected = (Card)cards[0];
            player.getDiscard().Add(cardSelected);
            GameBoard.getInstance().GetCards()[cardSelected] -= 1;


        }
        private int checkCounter;


    }
}
