using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards;

namespace UnitTestProject.Mocks
{
    class PlayerDefaultDecisionMock : PlayerDecisionMock
    {
        private List<Card> cards;
        public PlayerDefaultDecisionMock(List<Card> cardsPicked)
        {
            cards = cardsPicked;
        }

        public override List<Card> MakeDecision(IDecision decision)
        {
            return cards;
        }
    }
}
