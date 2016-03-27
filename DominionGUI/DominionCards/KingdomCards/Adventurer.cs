using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DominionCards.KingdomCards
{
    public class Adventurer : ActionCard
    {
        public Adventurer()
            : base("Adventurer", 0, 0, 0, 0, 6, 6)
        {
            // Uses ActionCard Constructor
        }
        public override void Play(Player player)
        {
            int cardsAdded = 0;
            while (cardsAdded != 2)
            {
                Card c = player.GetNextCard();
                if (c.getID() == 0 || c.getID() == 1 || c.getID() == 2)
                {
                    player.addCardToHand(c);
                    cardsAdded++;

                }
                else
                {
                    player.getDiscard().Add(c);
                }
            }

        }
        public override String ToString()
        {
            return "Adventurer";
        }
    }
}
