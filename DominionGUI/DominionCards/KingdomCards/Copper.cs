using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Copper : TreasureCard
    {
        public Copper()
            : base(1, 0, 0)
        {
            // Uses TreasureCard Constructor
        }

        public override String ToString()
        {
            return "Copper";
        }
    }
}
