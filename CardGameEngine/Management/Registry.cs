using System.Collections.Generic;

using CardGameEngine.Entities;

namespace CardGameEngine.Management {
    public class Registry {
        private Dictionary<string, CardType> cardTypes;
        private Dictionary<string, Effect> effects;

        internal Registry() {
            cardTypes = new Dictionary<string, CardType>();
            effects = new Dictionary<string, Effect>();
        }

        public CardType GetCardType(string key) {
            CardType value;
            if (cardTypes.TryGetValue(key, out value)) {
                return value;
            }
            else {
                // TODO: error handling
                return null;
            }
        }

        public Effect GetEffect(string key) {
            Effect value;
            if (effects.TryGetValue(key, out value)) {
                return value;
            }
            else {
                // TODO: error handling
                return null;
            }
        }

        internal void RegisterCardType(string key, CardType cardType) {
            cardTypes.Add(key, cardType);
        }

        internal void RegisterEffect(string key, Effect cardType) {
            effects.Add(key, cardType);
        }
    }
}
