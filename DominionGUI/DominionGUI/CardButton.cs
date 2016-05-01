using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominionCards;
using System.Threading;

namespace DominionGUI
{
    public class CardButton : Button
    {
        public Card card;
        public bool ForSale;
        // is active makes the card unusable. if for sale is true, card is availible to be bought,
        //      else it is in a player's hand and can be played.
        public CardButton(Card card) : base()
        {
            this.card = card;
        }
        public override String ToString()
        {
            return "Card Button (" + card.ToString() + ")";
        }

        public void InitializeEventHandler()
        {
            if (ForSale)
            {
                this.Click += new System.EventHandler(BuyThisCard);
            }
            else
            {
                this.Click += new System.EventHandler(PlayThisCard);
            }
        }

        internal void BuyThisCard(object sender, EventArgs e)
        {
            if (GameBoard.getGamePhase() != 3)
            {
                Console.WriteLine("buy button ignored because game in wrong state (" + GameBoard.getGamePhase() + ")");
                return;
            }
            Console.WriteLine("\n                       BUTTON PUSH BUY!!!\n");
            Console.WriteLine("tried to buy a card with id " + card.getID());
            GameBoard.setLastCardBought(card);
            lock (GameBoard.BuyPhaseLock)
            {
                Monitor.PulseAll(GameBoard.BuyPhaseLock);
                Monitor.Wait(GameBoard.BuyPhaseLock);
            }
            //GraphicsBoard.WaitToUpdateLabels();
        }

        internal void PlayThisCard(object sender, EventArgs e)
        {   
            if (GameBoard.getGamePhase() != 1)
            {
                Console.WriteLine("play button ignored because game in wrong state (" + GameBoard.getGamePhase() + ")");
                return;
            }
            Console.WriteLine("\n                       BUTTON PUSH PLAY!!!\n");
            Console.WriteLine("in the player's hand");
            GameBoard.setLastCardPlayed(card);
            lock (GameBoard.ActionPhaseLock)
            {
                Monitor.PulseAll(GameBoard.ActionPhaseLock);
                Monitor.Wait(GameBoard.ActionPhaseLock);
            }
            //GraphicsBoard.WaitToUpdateLabels();
        }

    }
}
