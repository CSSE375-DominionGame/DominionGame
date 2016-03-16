using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Bureaucrat : AttackCard
    {
        public Bureaucrat()
            : base(0, 0, 0, 0, 4, 7)
        {
            // Uses ActionCard Constructor
        }
        public override void Play(Player player)
        {
            base.Play(player);
            player.getDeck().Push(new Silver());
        }

        public override void MakeDelayedAttack(Player playerAttacked)
        {
            for (int i = 0; i < playerAttacked.getHand().Count; i++)
            {
                Card c = (Card)playerAttacked.getHand()[i];
                if (c.getID() == 3 || c.getID() == 4 || c.getID() == 5)
                {
                    playerAttacked.getDeck().Push(c);
                    playerAttacked.getHand().Remove(c);
                    break;
                }
            }
        }
        public override String ToString()
        {
            return "Bureaucrat";
        }
    }
}
