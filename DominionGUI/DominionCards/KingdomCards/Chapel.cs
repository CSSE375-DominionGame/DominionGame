using DominionCards.Decisions;
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
            this.decision = new ChapelDecision();
        }

        public override String ToString()
        {
            return "Chapel";
        }
    }
}
