using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Woodcutter : ActionCard
    {
        private static int ID = 30;
        public Woodcutter()
            : base("Wood cutter", 0, 2, 1, 0, 3, ID)
        {
            // Uses ActionCard Constructor
        }
        public override String ToString()
        {
            return "Woodcutter";
        }

    }
}
