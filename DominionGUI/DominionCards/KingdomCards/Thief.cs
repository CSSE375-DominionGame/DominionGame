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
        private List<Card> cardsTrashed;
        public Thief()
            : base("Thief", 0, 0, 0, 0, 4, ID)
        {
            // Uses AttackCard Constructor
            cardsTrashed = new List<Card>(); //Keeps track of this card's own trash
        }
        /*public override void Play(Player player)
        {
            base.Play(player); // resolves attacks on other players.

            if (cardsTrashed.Count == 0)
            {
                MessageBox.Show("You have no cards to keep from the thief!");
                return;
            }
            //List<Card> cards = player.SelectCards(cardsTrashed, "Choose card(s) to keep", cardsTrashed.Count);
            List<Card> cards = player.SelectCards(this.decision);

            for (int i = 0; i < cards.Count; i++)
            {
                List<Card> currentDiscard = player.getDiscard();
                currentDiscard.Add(cards[i]);
                player.setDiscard(currentDiscard);
            }
        }*/

        public override void MakeImmediateAttack(Player playerAttacked)
        {
            List<Card> cards = new List<Card>();
            cards.Add(playerAttacked.getDeck().Pop());
            cards.Add(playerAttacked.getDeck().Pop());


            if (!((Card)cards[1]).IsTreasure())
            {
                List<Card> currentDiscard = playerAttacked.getDiscard();
                currentDiscard.Add(cards[1]);
                playerAttacked.setDiscard(currentDiscard);
                cards.Remove(cards[1]);
            }
            if (!((Card)cards[0]).IsTreasure())
            {
                List<Card> currentDiscard = playerAttacked.getDiscard();
                currentDiscard.Add(cards[0]);
                playerAttacked.setDiscard(currentDiscard);
                cards.Remove(cards[0]);
            }



            if (cards.Count == 0)
            {
                MessageBox.Show("Player " + playerAttacked.getNumber() + " does not have any cards you can trash.");
                return;
            }
            //List<Card> cardsToTrash = playerAttacked.SelectCards(cards, "Choose a card to trash", 1);
            List<Card> cardsToTrash = playerAttacked.MakeDecision(this.decision);
            while (cardsToTrash.Count != 1)
            {
                DialogResult result1 = MessageBox.Show("You must select exactly 1 card to trash.  Try again");
                //cardsToTrash = playerAttacked.SelectCards(cards, "Choose a card to trash.", 1);
                cardsToTrash = playerAttacked.MakeDecision(this.decision);
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
