using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Duchy : VictoryCard
    {
        public Duchy()
            : base(3, 5, 4)
        {
            // Uses VictoryCard Constructor
        }
        public override String ToString()
        {
            return "Duchy";
        }
    }
}
