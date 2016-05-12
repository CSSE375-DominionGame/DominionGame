using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards.Decisions;

namespace DominionCards.KingdomCards
{
    public class Chancellor : ActionCard
    {
        public Chancellor()
            : base("Chancellor", 0, 2, 0, 0, 3, 9)
        {
            decision = new ChancelorDecision();
        }

        public override String ToString()
        {
            return "Chancellor";
        }
     }
}
