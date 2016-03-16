using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Estate : VictoryCard
    {
        private static int ID = 3;
        public Estate()
            : base(1, 2, ID)
        {
            // Uses VictoryCard Constructor
        }
        public override String ToString()
        {
            return "Estate";
        }
    }
}
