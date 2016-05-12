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
    public class Mine : ActionCard
    {
        private static int ID = 19;
        public Mine()
            : base("Mine", 0, 0, 0, 0, 5, ID)
        {
            // Uses ActionCard Constructor
            decision = new MineDecision();
        }

        public override String ToString()
        {
            return "Mine";
        }
    }
}