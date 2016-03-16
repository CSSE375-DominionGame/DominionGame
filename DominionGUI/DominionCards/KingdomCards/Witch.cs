using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Witch : AttackCard
    {
        private static int ID = 29;
        public Witch()
            : base(2, 0, 0, 0, 5, ID)
        {
            // Uses AttackCard Constructor
        }
        public override void MakeDelayedAttack(Player playerAttacked)
        {
            MessageBox.Show("You have been cursed by a witch!!");
            Console.WriteLine("Curse debug: top discard " + playerAttacked.getDiscard()[playerAttacked.getDiscard().Count - 1]);
        }

        public override void MakeImmediateAttack(Player playerAttacked)
        {

            Dictionary<Card, int> dict = GameBoard.getInstance().GetCards();
            if (dict[new Curse()] > 0)
            {
                playerAttacked.getDiscard().Add(new KingdomCards.Curse());
                dict[new Curse()] -= 1;
            }
        }
        public override String ToString()
        {
            return "Witch";
        }
    }
}
