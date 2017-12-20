using System.Collections.Generic;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public class CardType : SelfRegistry<CardType> {
        // ? Is this necessary?
        /// <summary>
        /// Name of the CardType
        /// </summary>
        private string name;

        public List<Effect> Effects;

        // ! Made constructor public for now, because there is no way to create a CardType without it yet
        /// <summary>
        /// private Constructor, as this Class manages its instances itself
        /// </summary>
        /// <param name="name">name of the type of card</param>
        public CardType(string name) : base(name) {
            this.name = name;
        }
    }
}
