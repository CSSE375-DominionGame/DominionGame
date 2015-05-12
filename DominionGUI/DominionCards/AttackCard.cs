﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public abstract class AttackCard : ActionCard
    {
        public AttackCard(int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(extraCards, extraMoney, extraBuys, extraActions, price, idNumb)
        {
            // TODO implement this class
        }
        public abstract void MakeAttack(Player p);

        public void PushAttackToAttacks(Player p)
        {
            GameBoard board = GameBoard.getInstance();
            while (board.turnOrder.Peek() != p)
            {
                Player current = board.NextPlayer();
                current.getAttacks().Push(this);
            }
            board.NextPlayer();
        }

        public override void play(Player player)
        {
            base.play(player);
            PushAttackToAttacks(player);
        }

    }
}