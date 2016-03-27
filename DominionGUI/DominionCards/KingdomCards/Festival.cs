using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Festival : ActionCard
    {
        private static int ID = 13;
        public Festival()
            : base("Festival", 0, 2, 1, 2, 5, ID)
        {
            // Uses ActionCard Constructor
        }

        public override String ToString()
        {
            return "Festival";
        }
    }
}
