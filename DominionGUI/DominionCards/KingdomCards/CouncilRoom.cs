using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards.KingdomCards
{
    public class CouncilRoom : ActionCard
    {
        public CouncilRoom()
            : base("Council Room", 4, 0, 1, 0, 5, 11)
        {
            // Uses ActionCard Constructor
        }

        public override String ToString()
        {
            return "Council Room";
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
