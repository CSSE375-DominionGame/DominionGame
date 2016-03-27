using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Village : ActionCard
    {
        private static int ID = 28;
        public Village()
            : base("Village", 1, 0, 0, 2, 3, ID)
        {
            // Uses ActionCard Constructor
        }
        public override String ToString()
        {
            return "Village";
        }
    }
}
