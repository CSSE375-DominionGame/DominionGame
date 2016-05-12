using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Workshop : ActionCard
    {
        private static int ID = 31;
        public Workshop()
            : base("Workshop", 0, 0, 0, 0, 3, ID)
        {
            decision = new WorkshopDecision();
        }
        
        public override String ToString()
        {
            return "Workshop";
        }
    }
}
