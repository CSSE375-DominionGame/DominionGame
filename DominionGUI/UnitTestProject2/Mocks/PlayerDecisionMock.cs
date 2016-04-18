using DominionCards;
using DominionCards.Decisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public abstract class PlayerDecisionMock : Player
    {
        public PlayerDecisionMock()
            : base()
        { }

        public override void actionPhase()
        {
            throw new NotImplementedException();
        }

        public override void buyPhase()
        {
            throw new NotImplementedException();
        }
    }
}
