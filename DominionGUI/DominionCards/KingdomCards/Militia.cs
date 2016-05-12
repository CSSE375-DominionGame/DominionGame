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
    public class Militia : AttackCard
    {
        private static int ID = 18;
        public Militia()
            : base("Militia", 0, 2, 0, 0, 4, ID)
        {
            attackDecision = new MilitiaDecision();
            attackDelayed = true;
        }

        public override String ToString()
        {
            return "Militia";
        }
    }
}
