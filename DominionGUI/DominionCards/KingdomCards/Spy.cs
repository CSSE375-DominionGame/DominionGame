using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Spy : AttackCard
    {
        private static int ID = 24;
        public Spy()
            : base(1, 0, 0, 1, 4, ID)
        {
            // Uses AttackCard Constructor
        }

        public override void Play(Player player)
        {
            base.Play(player);
            MakeImmediateAttack(player);
        }
        public override String ToString()
        {
            return "Spy";
        }

        public override void MakeImmediateAttack(Player playerAttacked)
        {
            if (playerAttacked.getDeck().Count == 0)
            {
                playerAttacked.setDeck(Player.Shuffle(playerAttacked.getDiscard()));
                playerAttacked.setDiscard(new System.Collections.ArrayList());
            }
            String str;
            if (GameBoard.getInstance().turnOrder.Peek() == playerAttacked)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("you have a ");
                builder.Append(playerAttacked.getDeck().Peek());
                builder.Append(" on the top of their deck.");
                builder.Append("\n  would you like to discard it?");
                str = builder.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("player ");
                builder.Append(playerAttacked.getNumber());
                builder.Append(" has a ");
                builder.Append(playerAttacked.getDeck().Peek());
                builder.Append(" on the top of their deck.");
                builder.Append("\n  would you like to discard it?");
                str = builder.ToString();
            }
            DialogResult result1 = MessageBox.Show(str, "You played a spy", MessageBoxButtons.YesNo);
            String result = result1.ToString();

            if (result.ToLower().Equals("yes"))
            {
                playerAttacked.getDiscard().Add(playerAttacked.getDeck().Pop());
            }

        }
    }
}
