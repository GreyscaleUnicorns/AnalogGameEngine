using System.Collections.Generic;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public abstract class CardType : SelfRegistry<CardType> {
        public CardType() {
            this.effects = new List<Effect>();
        }

        protected List<Effect> effects;
        public IReadOnlyCollection<Effect> Effects {
            get {
                return effects.AsReadOnly();
            }
        }
    }
}
