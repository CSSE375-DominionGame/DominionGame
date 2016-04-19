using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class AiPlayer : Player
    {
        public AiPlayer(int playerNumber)
            : base()
        {
            setNumber(1);
        }

        public override void actionPhase()
        {
            throw new NotImplementedException();
        }

        public override void buyPhase()
        {
            throw new NotImplementedException();
        }

        public override List<Card> MakeDecision(IDecision decision)
        {
            throw new NotImplementedException();
        }
    }
}
