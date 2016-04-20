using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public class TieException : Exception
    {

        private ArrayList TiedPlayers = new ArrayList();
        private int VictoryPoints, Money;
        private GamePhaseEnd endPhase = new GamePhaseEnd();

        public TieException(Player p1, Player p2, int VPs, int money) : base() 
        {
            Money = money;
            VictoryPoints = VPs;
            TiedPlayers.Add(p1);
            TiedPlayers.Add(p2);
        }

        public bool BreaksTie(Player p)
        {
            endPhase.setEndPhase(p);
            int playerVP = endPhase.countVictoryPoints();
            int playerMoney = endPhase.getTotalMoney();
            if(playerVP < this.VictoryPoints || 
                playerVP == this.VictoryPoints && playerMoney <= this.Money)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void addPlayer(Player p)
        {
            TiedPlayers.Add(p);
        }
        public bool Ties(Player p)
        {
            endPhase.setEndPhase(p);
            if (endPhase.countVictoryPoints() == this.VictoryPoints && endPhase.getTotalMoney() == this.Money)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int getArraySize()
        {
            return TiedPlayers.Count;
        }
        public String PrintWinners()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Player ");
            sb.Append(((Player)TiedPlayers[0]).getNumber());
            for (int i = 1; i < TiedPlayers.Count - 1; i++)
            {
                sb.Append(", ");
                sb.Append("Player ");
                sb.Append(((Player)TiedPlayers[i]).getNumber());
                i++;
            }
            sb.Append(", and Player ");
            sb.Append(((Player)TiedPlayers[TiedPlayers.Count - 1]).getNumber());
            sb.Append(" tied for winner! \n");
            String str = sb.ToString();
            Console.WriteLine(str);
            return str;
        }


    }
}
