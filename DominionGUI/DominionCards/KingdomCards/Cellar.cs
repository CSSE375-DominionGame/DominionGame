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
    public class Cellar : ActionCard
    {
        public Cellar()
            : base("Cellar", 0, 0, 0, 1, 2, 8)
        {
            this.decision = new CellarDecision();
        }

        public override String ToString()
        {
            return "Cellar";
        }
    }
}
