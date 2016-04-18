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
        List<Card> getCardSelection(Player player);
        List<Card> SelectByGraphic(Player player);
        string getText();
        Boolean isCancelable();
        bool cardSelectionValid(List<Card> cards);
        void applyDecisionTo(Player player, List<Card> cardsSelected);
        List<Card> MakeDecision(Player player);
    }
}
