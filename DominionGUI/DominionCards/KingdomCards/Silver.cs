using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Silver : TreasureCard
    {

        private static int ID = 1;
        public Silver()
            : base(2, 3, ID)
        {
            // Uses TreasureCard Constructor
        }
        public override String ToString()
        {
            return "Silver";
        }
    }
}
