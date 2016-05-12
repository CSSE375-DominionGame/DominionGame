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
    public class Feast : ActionCard
    {
        private static int ID = 12;
        public Feast()
            : base("Feast", 0, 0, 0, 0, 4, ID)
        {
            this.decision = new FeastDecision();
        }
        public override String ToString()
        {
            return "Feast";
        }
    }
}
