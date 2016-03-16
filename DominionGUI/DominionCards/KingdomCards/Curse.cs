using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Curse : VictoryCard
    {
        public Curse() : base(-1, 0, 32)
        {
            // Uses VictoryCard Constructor
        }

        public override String ToString()
        {
            return "Curse";
        }
    }
}
