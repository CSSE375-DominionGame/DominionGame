﻿using System;
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

        public TieException(Player p1, Player p2, int VPs, int money) : base() 
        {
            Money = money;
            VictoryPoints = VPs;
            TiedPlayers.Add(p1);
            TiedPlayers.Add(p2);
        }
        /**
         * returns true if player broke the tie (beat the players that tied.)
         * If the given player tied with all the players that were already tied, the player is added to the tie,
         *   but false is returned.
         * If the given player loses, false is returned.
         */
        public Boolean BreaksTie(Player p)
        {
            int playerVP = p.countVictoryPoints();
            int playerMoney = p.getTotalMoney();
            if (playerVP == VictoryPoints && playerMoney == Money)
            {
                TiedPlayers.Add(p);
                return false;
            }
            else if(playerVP < VictoryPoints || (playerVP == VictoryPoints && playerMoney < Money))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int getArraySize()
        {
            return TiedPlayers.Count;
        }
        public void PrintWinners()
        {
            Console.Write("Player ");
            Console.Write(((Player)TiedPlayers[0]).getNumber());
            for (int i = 0; i < TiedPlayers.Count - 1; i++)
            {
                Console.Write(", ");
                Console.Write("Player ");
                Console.Write(((Player)TiedPlayers[i]).getNumber());
            }
            Console.Write("and Player ");
            Console.Write(((Player)TiedPlayers[TiedPlayers.Count - 1]).getNumber());
            Console.Write("all tied for winner! \n");
        }


    }
}
