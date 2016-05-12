using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards.Decisions;

namespace DominionCards.KingdomCards
{
    public class Remodel : ActionCard
    {
        private static int ID = 22;
        public Remodel()
            : base("Remodel", 0, 0, 0, 0, 4, ID)
        {
            decision = getRemodelDecision();
        }

        private IDecision getRemodelDecision()
        {
            CompositeDecision dec1 = new CompositeDecision(new RemodelBuyDecision(), null);
            CompositeDecision dec2 = new CompositeDecision(new RemodelTrashDecision(), dec1);
            return dec2;
        }
        
        public override String ToString()
        {
            return "Remodel";
        }
    }
}
