﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Chancellor : ActionCard
    {
        public Chancellor()
            : base(0, 2, 0, 0, 3, 9)
        {
            // TODO implement
        }

        public override String ToString()
        {
            return "Chancellor";
        }
        public override void Play(Player player)
        {
            DialogResult result1 = MessageBox.Show("You played a chancellor. Would you like to discard your hand now?",
                "You played a Chancellor",
                MessageBoxButtons.YesNo);
            string result = result1.ToString();
            Console.WriteLine(result);

            if (result.ToLower().Equals("yes"))
            {
                Stack<Card> deck = player.getDeck();
                ArrayList discard = player.getDiscard();
                while (deck.Count > 0)
                {
                    discard.Add(deck.Pop());
                }
            }
        }
    }
}
