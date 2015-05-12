﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public abstract class Card
    {
        public static int GENERIC_CARD_ID = -1;

        private int price, id;
        public Card(int price, int idNumb)
        {
            this.price = price;
            id = idNumb;
        }
        public int getPrice()
        {
            return price;
        }
        public int getID()
        {
            return id;
        }
        public abstract int getVictoryPoints();
        public abstract void Play(Player player);

        public override int GetHashCode()
        {
            return id;
        }
        public override bool Equals(object obj)
        {
            Card c;
            try
            {
                c = (Card)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            if (c.getID() == getID())
            {
                return true;
            }
            return false;
        }
    }
}
