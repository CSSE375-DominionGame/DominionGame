﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public abstract class VictoryCard : Card
    {
        int vp;
        public VictoryCard(int victoryPts, int price, int idNumb)
            : base(price, idNumb)
        {
            vp = victoryPts;
        }
        public override int getVictoryPoints(){
            return vp;
        }
        public override bool IsVictory()
        {
            return true;
        }
    }
}
