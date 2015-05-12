﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class CouncilRoom : ActionCard
    {
        public CouncilRoom()
            : base(4, 0, 1, 0, 5, 11)
        {
            //does nothing
        }

        public override void Play(Player player)
        {
            GameBoard board = GameBoard.getInstance();
            board.NextPlayer();
            while (board.turnOrder.Peek() != player)
            {
                Player current = board.NextPlayer();
                current.getHand().Add(current.GetNextCard());
            }
        }
    }
}
