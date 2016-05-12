using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Laboratory : ActionCard
    {
        private static int ID = 15;
        public Laboratory()
            : base("Laboratory", 2, 0, 0, 1, 5, ID)
        {
        }
        public override String ToString()
        {
            return "Laboratory";
        }
    }
}
