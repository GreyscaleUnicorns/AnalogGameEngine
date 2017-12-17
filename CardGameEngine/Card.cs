using System;

//blöde Änderung

namespace CardGameEngine {
    /// <summary>
    /// Card Class
    /// implements specific GameCard
    /// which holds unique properties of a card
    /// </summary>
    public class Card {

        private string name;

        private CardType type;

        private Entities.CardCollection collection = null;

        /// <summary>
        /// Generates a new Card
        /// </summary>
        /// <param name="name">name of the new Card</param>
        public Card(string name) {
            Init(name, CardType.Get("default"));
        }

        /// <summary>
        /// Generates a new Card with a given type
        /// </summary>
        /// <param name="name">name of the new card</param>
        /// <param name="type">type of the new card</param>
        public Card(string name, CardType type) {
            Init(name, type);
        }

        // initilizer
        private void Init(string name, CardType type) {
            name = name;
            type = type;
        }

        /// <summary>
        /// moves the current Card to the specified collection
        /// </summary>
        /// <param name="collection">the targeted collection</param>
        /// <param name="position">position at which the card should be inserted</param>
        public void moveTo(Entities.CardCollection collection, int position = 0) {
            if (this.collection != null) {
                this.collection.RemoveCard(this);
            }
            this.collection = collection;
            this.collection.AddCard(this, position);
        }

        public void activateEffects() {
            foreach (var effect in this.type.Effects) {
                effect.trigger(null); //!TODO Card muss ihren GameState kennen
            }
        }

    }
}
