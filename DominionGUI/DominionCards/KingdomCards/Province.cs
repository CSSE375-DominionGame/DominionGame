using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Province : VictoryCard
    {
        private static int ID = 5;
        public Province()
            : base(6, 8, ID)
        {
            // Uses VictoryCard Constructor
        }
        public override String ToString()
        {
            return "Province";
        }
    }
}
