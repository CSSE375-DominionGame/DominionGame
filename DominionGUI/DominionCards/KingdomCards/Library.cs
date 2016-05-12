using DominionCards.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class Library : ActionCard
    {
        private static int ID = 16;
        public Library()
            : base("Library", 0, 0, 0, 0, 5, ID)
        {
            decision = new LibraryDecision();
        }
        public override String ToString()
        {
            return "Library";
        }
    }
}
