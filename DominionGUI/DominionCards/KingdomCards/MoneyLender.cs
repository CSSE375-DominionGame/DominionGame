using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using DominionCards.Decisions;

namespace DominionCards.KingdomCards
{
    public class MoneyLender : ActionCard
    {
        private static int ID = 21;
        public MoneyLender()
            : base("Money Lender", 0, 0, 0, 0, 4, ID)
        {
            decision = new MoneyLenderDecision();
        }
        public override String ToString()
        {
            return "Money Lender";
        }
        public override void Play(Player player)
        {
            DialogResult result1 = MessageBox.Show("You played a moneylender. Would you like to trash a copper now?",
                "You played a moneylender",
                MessageBoxButtons.YesNo);
            string result = result1.ToString();
            Console.WriteLine(result);

            if (result.ToLower().Equals("yes"))
            {
                List<Card> hand = player.getHand();
                for (int i = 0; i < hand.Count; i++)
                {
                    Card c;
                    try
                    {
                        c = (Card)hand[i];
                    }
                    catch (InvalidCastException)
                    {
                        continue;
                    }
                    if (c.getID() == 0)
                    {
                        hand.Remove(c);
                        player.money += 3;
                        return;
                    }
                }
            }
        }
    }
}
