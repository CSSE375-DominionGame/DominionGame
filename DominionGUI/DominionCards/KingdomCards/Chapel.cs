using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Chapel : ActionCard
    {
        public static int ID = 10;
        public Chapel()
            : base("Chapel", 0, 0, 0, 0, 2, ID)
        {
            // Uses ActionCard Constructor
        }

        public override String ToString()
        {
            return "Chapel";
        }
        public override void Play(Player player)
        {
            List<Card> cardsToList = new List<Card>();
            foreach (Card card in player.getHand())
            {
                cardsToList.Add(card);

            }
            if (cardsToList.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the chapel!");
                return;
            }
            List<Card> cards = player.SelectCards(cardsToList, "Select up to 4 cards to trash.", 4);
            while (cards.Count > 4)
            {
                DialogResult result1 = MessageBox.Show("You may only select up to 4 cards to trash.  Try again");
                cards = player.SelectCards(cardsToList, "Select up to 4 cards to trash.", 4);
            }
            for (int i = 0; i < cards.Count; i++)
            {
                Card toTrash = (Card)cards[i];
                player.getHand().Remove(toTrash);
            }






        }
    }
}
