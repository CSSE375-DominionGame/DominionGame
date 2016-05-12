using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Gold : TreasureCard
    {
        private static int ID = 2;
        public Gold()
            : base(3, 6, ID) {}
        public override String ToString()
        {
            return "Gold";
        }
    }
}
