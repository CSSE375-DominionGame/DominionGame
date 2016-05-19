using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards.Decisions;

namespace DominionCards.KingdomCards
{
    public class ThroneRoom : ActionCard
    {
        private static int ID = 26;
        public ThroneRoom()
            : base("Throne Room", 0, 0, 0, 0, 4, ID)
            //This is a test :)
        {
            this.decision = new ThroneRoomDecision();
        }
        public override String ToString()
        {
            return "Throne Room";
        }
    }
}
