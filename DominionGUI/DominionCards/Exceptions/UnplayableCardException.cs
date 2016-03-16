using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class UnplayableCardException : Exception
    {
        public UnplayableCardException(Card unplayable)
            : base("You tried to play a '" + unplayable + "', which cannot be played.")
        {
            // Do nothing
        }
    }
}
