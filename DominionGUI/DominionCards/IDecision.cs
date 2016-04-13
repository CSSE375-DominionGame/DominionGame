using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public interface IDecision
    {
        int getMaxCards();
        int getMinCards();
        string getText();
        string getErrorMsg();
        Boolean isCancelable();
        bool cardSelectionValid(List<Card> cards);
        List<Card> getCardSelection(Player player);
        void applyDecisionTo(Player player, List<Card> cardsSelected);
        List<Card> SelectCards(Player player);
    }
}
