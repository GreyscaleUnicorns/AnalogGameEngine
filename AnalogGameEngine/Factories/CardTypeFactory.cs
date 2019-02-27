using System.Linq;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.Factories {
    public abstract class CardTypeFactory {
        public delegate void CardTypeCreationEvent(CardType[] cardTypes);
        protected event CardTypeCreationEvent OnCardTypeCreation = delegate { };
        internal void Initialize(Registry registry) {
            this.CreateCardTypes(registry);
            this.OnCardTypeCreation(registry.GetCardTypes());
        }

        protected abstract void CreateCardTypes(Registry registry);
    }
}
