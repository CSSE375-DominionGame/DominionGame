using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards;
using DominionCards.Decisions;

namespace UnitTestProject.Mocks
{
    public class PlayerCompositeMock : PlayerDecisionMock
    {
        private Queue<List<Card>> decisions;

        public PlayerCompositeMock(Queue<List<Card>> q)
            : base() 
        {
            decisions = q;
        } 

        public override List<Card> MakeDecision(IDecision decision)
        {
            if (decision is CompositeDecision)
            {
                return decision.SelectByGraphic(this);
            }
            return decisions.Dequeue();
        }
    }
}
