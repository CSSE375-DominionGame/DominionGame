using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Thief : AttackCard
    {
        private static int ID = 25;
        private ArrayList cardsTrashed;
        public Thief()
            : base(0, 0, 0, 0, 4, ID)
        {
            // Uses AttackCard Constructor
            cardsTrashed = new ArrayList(); //Keeps track of this card's own trash
        }
        public override void Play(Player player)
        {
            base.Play(player); // resolves attacks on other players.

            if (cardsTrashed.Count == 0)
            {
                MessageBox.Show("You have no cards to keep from the thief!");
                return;
            }
            ArrayList cards = player.SelectCards(cardsTrashed, "Choose card(s) to keep", cardsTrashed.Count);

            for (int i = 0; i < cards.Count; i++)
            {
                player.getDiscard().Add(cards[i]);
            }
        }

        public override void MakeImmediateAttack(Player playerAttacked)
        {
            ArrayList cards = new ArrayList();
            cards.Add(playerAttacked.getDeck().Pop());
            cards.Add(playerAttacked.getDeck().Pop());


            if (!((Card)cards[1]).IsTreasure())
            {
                playerAttacked.getDiscard().Add(cards[1]);
                cards.Remove(cards[1]);
            }
            if (!((Card)cards[0]).IsTreasure())
            {
                playerAttacked.getDiscard().Add(cards[0]);
                cards.Remove(cards[0]);
            }



            if (cards.Count == 0)
            {
                MessageBox.Show("Player " + playerAttacked.getNumber() + " does not have any cards you can trash.");
                return;
            }
            ArrayList cardsToTrash = playerAttacked.SelectCards(cards, "Choose a card to trash", 1);
            while (cardsToTrash.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to trash.  Try again");
                cardsToTrash = playerAttacked.SelectCards(cards, "Choose a card to trash.", 1);
            }
            Card cardSelected = (Card)cardsToTrash[0];
            cardsTrashed.Add(cardSelected);
            cards.Remove(cardSelected);
            if (cards.Count == 1)
            {
                playerAttacked.getDiscard().Add(cards[0]);
            }
        }

        public override String ToString()
        {
            return "Thief";
        }
    }
}
