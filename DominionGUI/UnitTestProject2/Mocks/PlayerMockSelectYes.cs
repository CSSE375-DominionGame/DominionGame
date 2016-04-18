using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.Decisions;
using DominionCards;

namespace UnitTestProject.Mocks
{
    public class PlayerMockSelectYes : PlayerMockYesNo
    {
        public override List<Card> MakeDecision(IDecision decision)
        {
            return ((YesNoDecision)decision).getYesCards(this);
        }
    }
}
