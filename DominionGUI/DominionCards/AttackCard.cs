using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.Decisions;

namespace DominionCards
{
    public abstract class AttackCard : ActionCard
    {
        protected IDecision attackDecision;
        protected bool attackDelayed;
        public AttackCard(string name, int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(name, extraCards, extraMoney, extraBuys, extraActions, price, idNumb)
        {
            attackDecision = new NullDecision();
        }
        public virtual void MakeDelayedAttack(Player playerAttacked)
        {
            if (attackDelayed)
            {
                List<Card> cardsSelected = playerAttacked.MakeDecision(attackDecision);
                while (!this.attackDecision.cardSelectionValid(cardsSelected))
                {
                    displaySelectionError();
                    cardsSelected = playerAttacked.MakeDecision(attackDecision);
                }
                this.attackDecision.applyDecisionTo(playerAttacked, cardsSelected);
            }
        }
        public virtual void MakeImmediateAttack(Player playerAttacked)
        {
            if (! attackDelayed)
            {
                List<Card> cardsSelected = playerAttacked.MakeDecision(attackDecision);
                while (!this.attackDecision.cardSelectionValid(cardsSelected))
                {
                    displaySelectionError();
                    cardsSelected = playerAttacked.MakeDecision(attackDecision);
                }
                this.attackDecision.applyDecisionTo(playerAttacked, cardsSelected);
            }
        }
        public void EnqueueAttacks(Player attacker)
        {
            GameBoard board = GameBoard.getInstance();
            foreach (Player target in board.turnOrder)
            {
                if (target != attacker && (!target.getHand().Contains(new KingdomCards.Moat())))
                {
                    MakeImmediateAttack(target);
                    target.getAttacks().Enqueue(this);
                }
            }
        }

        public override void Play(Player player)
        {
            EnqueueAttacks(player);
        }

    }
}
