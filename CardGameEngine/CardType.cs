using System.Collections.Generic;

namespace CardGameEngine {

    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public class CardType {

        /// <summary>
        /// static member for managing the types
        /// </summary>
        private static Dictionary<string, CardType> cardTypes =
            new Dictionary<string, CardType>();

        /// <summary>
        /// Name of the CardType
        /// </summary>
        private string name;

        public List<IEffect> Effects;

        // ! Made constructor public for now, because there is no way to create a CardType without it yet
        /// <summary>
        /// private Constructor, as this Class manages its instances itself
        /// </summary>
        /// <param name="name">name of the type of card</param>
        public CardType(string name) {
            this.name = name;
        }

        /// <summary>
        /// searches or creates a card type
        /// </summary>
        /// <param name="name">name to be searched for</param>
        /// <returns>Instance of CardType matching the search</returns>
        public static CardType Get(string name) {
            if (cardTypes.ContainsKey(name)) return cardTypes[name];
            var card = new CardType(name);
            cardTypes[name] = card;
            return card;
        }

    }
}
