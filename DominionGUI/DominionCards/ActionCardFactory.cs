using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominionCards.KingdomCards;

namespace DominionCards
{
    public class ActionCardFactory
    {
        private static readonly int CELLAR_ID = 8;
        private static readonly int CHANCELLOR_ID = 9;
        private static readonly int CHAPEL_ID = 10;
        private static readonly int FEAST_ID = 12;
        private static readonly int FESTIVAL_ID = 13;
        private static readonly int LABORATORY_ID = 15;
        private static readonly int MINE_ID = 19;
        private static readonly int MARKET_ID = 17;
        private static readonly int REMODEL_ID = 22;
        private static readonly int SMITHY_ID = 23;
        private static readonly int THRONEROOM_ID = 26;
        private static readonly int VILLAGE_ID = 28;
        private static readonly int WOODCUTTER_ID = 30;
        private static readonly int WORKSHOP_ID = 31;

        public ActionCardFactory() { } 

        public ActionCard createNewAdventurer()
        {
            return new Adventurer();
        }

        public ActionCard createNewCellar()
        {
            return new ActionCard("Cellar", 0, 0, 0, 1, 2, CELLAR_ID);
        }

        public ActionCard createNewChancellor()
        {
            return new Chancellor();
        }

        public ActionCard createNewChapel()
        {
            return new ActionCard("Chapel", 0, 0, 0, 0, 2, CHAPEL_ID);
        }

        public ActionCard createNewCouncilRoom()
        {
            return new CouncilRoom();
        }

        public ActionCard createNewFeast()
        {
            return new ActionCard("Feast", 0, 0, 0, 0, 4, FEAST_ID);
        }

        public ActionCard createNewFestival()
        {
            return new ActionCard("Festival", 0, 2, 1, 2, 5, FESTIVAL_ID);
        }

        public ActionCard createNewLaboratory()
        {
            return new ActionCard("Laboratory", 2, 0, 0, 1, 5, LABORATORY_ID);
        }

        public ActionCard createNewLibrary()
        {
            return new Library();
        }

        public ActionCard createNewMarket()
        {
            return new ActionCard("Market", 1, 1, 1, 1, 5, MARKET_ID);
        }

        public ActionCard createNewMine()
        {
            return new ActionCard("Mine", 0, 0, 0, 0, 5, MINE_ID);
        }

        public ActionCard createNewMoneyLender()
        {
            return new MoneyLender();
        }

        public ActionCard createNewRemodel()
        {
            return new Remodel(); //new ActionCard("Remodel", 0, 0, 0, 0, 4, REMODEL_ID);
        }

        public ActionCard createNewSmithy()
        {
            return new ActionCard("Smithy", 3, 0, 0, 0, 4, SMITHY_ID);
        }

        public ActionCard createNewThroneRoom()
        {
            return new ActionCard("Throne Room", 0, 0, 0, 0, 4, THRONEROOM_ID);
        }

        public ActionCard createNewVillage()
        {
            return new ActionCard("Village", 1, 0, 0, 2, 3, VILLAGE_ID);
        }

        public ActionCard createNewWoodcutter()
        {
            return new ActionCard("Woodcutter", 0, 2, 1, 0, 3, WOODCUTTER_ID);
        }

        public ActionCard createNewWorkshop()
        {
            return new ActionCard("Workshop", 0, 0, 0, 0, 3, WORKSHOP_ID);
        }
    }
}