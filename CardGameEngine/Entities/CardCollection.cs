using System.Collections.Generic;

using CardGameEngine;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Base for all types of card collections.
    /// </summary>
    public class CardCollection {
        /// <summary>
        /// The list of all cards in the collection.
        /// </summary>
        /// <returns>A list of Cards</returns>
        public LinkedList<Card> Cards { get; private set; }

        public CardCollection() : this(null) {
            // Nothing to do
        }

        /// <param name="cards">Initial cards in collection</param>
        public CardCollection(Card[] cards) {
            if (cards != null) {
                this.Cards = new LinkedList<Card>(cards);
            } else {
                this.Cards = new LinkedList<Card>();
            }
        }

        /// <summary>
        /// Adds a card to the end of the collection.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card) {
            this.Cards.AddLast(card);
        }

        public void Shuffle() {
            // TODO: Implement
            this.Cards.RemoveLast();
        }
    }
}
