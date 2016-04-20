using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class NullCard : Card
    {

        public NullCard() : base(0, 0) { }

        public override int getVictoryPoints()
        {
            return 0;
        }

        public override void Play(Player player)
        {
            throw new UnplayableCardException(this);
        }
    }
}
