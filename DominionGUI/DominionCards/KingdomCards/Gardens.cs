using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Gardens : VictoryCard
    {
        private static int ID = 14;
        public Gardens()
            : base(0, 4, ID)
        {
            // Uses VictoryCard Constructor
            // implemented in countVictoryPoints() in Player
        }
        public override String ToString()
        {
            return "Gardens";
        }
    }
}
