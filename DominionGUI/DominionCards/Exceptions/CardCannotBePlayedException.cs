using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class CardCannotBePlayedException : Exception
    {
        public CardCannotBePlayedException(String msg)
            : base(msg)
        {
            // do nothing
        }

    }
}
