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
        private Stack<Player> targets;
        private Decision attackDecision;
        private bool attackDelayed;
        public AttackCard(string name, int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(name, extraCards, extraMoney, extraBuys, extraActions, price, idNumb)
        {
            attackDecision = new NullDecision();
            // TODO implement this class
        }
        public virtual void MakeDelayedAttack(Player playerAttacked)
        {
            if (attackDelayed)
            {
                List<Card> cardsSelected = attackDecision.SelectCards(playerAttacked);
                while (!this.attackDecision.cardSelectionValid(cardsSelected))
                {
                    displaySelectionError();
                    cardsSelected = attackDecision.SelectCards(playerAttacked);
                }
                this.attackDecision.applyDecisionTo(playerAttacked, cardsSelected);
            }
        }
        public virtual void MakeImmediateAttack(Player playerAttacked)
        {
            if (! attackDelayed)
            {
                List<Card> cardsSelected = attackDecision.SelectCards(playerAttacked);
                while (!this.attackDecision.cardSelectionValid(cardsSelected))
                {
                    displaySelectionError();
                    cardsSelected = attackDecision.SelectCards(playerAttacked);
                }
                this.attackDecision.applyDecisionTo(playerAttacked, cardsSelected);
            }
        }
        public void EnqueueAttacks(Player p)
        {
            GameBoard board = GameBoard.getInstance();
            board.NextPlayer();
            while (board.turnOrder.Peek() != p){
                Player current = board.NextPlayer();
                if (!current.getHand().Contains(new KingdomCards.Moat()))
                {
                    MakeImmediateAttack(current);
                    current.getAttacks().Enqueue(this);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Your attack was blocked by player " + p.getNumber() + "'s moat!");
                    current.getAttacks().Enqueue(new KingdomCards.Moat());
                }
            } 
        }

        public override void Play(Player player)
        {
            EnqueueAttacks(player);
        }

    }
}
