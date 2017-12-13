using System.Collections.Generic;

namespace CardGameEngine{

    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public class CardType{

#region Members
        //static
        private static Dictionary<string, CardType> cardTypes =
            new Dictionary<string, CardType>();

        //normal
        private string m_name;
#endregion


#region Constructors

        private CardType(string name){
            m_name = name;
        }

#endregion

#region StaticFunctions

        public static CardType Get(string name){
            if(cardTypes.ContainsKey(name)) return cardTypes[name];
            var card = new CardType(name);
            cardTypes[name] = card;
            return card;
        }

#endregion
    }
}
