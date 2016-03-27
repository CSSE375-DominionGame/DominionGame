using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Market : ActionCard
    {
        private static int ID = 17;
        public Market()
            : base("Market", 1, 1, 1, 1, 5, ID)
        {
            // Uses ActionCard Constructor
        }
        public override String ToString()
        {
            return "Market";
        }
    }
}
