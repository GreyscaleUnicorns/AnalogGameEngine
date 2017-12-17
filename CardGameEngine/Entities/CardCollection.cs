using System;
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
        /// <param name="card">the card to append</param>
        /// <param name="position">the position at which the card should be inserted</param>
        virtual public void AddCard(Card card, int position) {
            // overwirte behaviour in inheriting classes
            this.Cards.AddLast(card);
        }

        // TODO: documentation
        /// <summary>
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool RemoveCard(Card card) {
            return this.Cards.Remove(card);
        }

        /// <summary>
        /// removes a Card from the Collection
        /// </summary>
        /// <param name="card">card to be removed</param>
        public void RemoveCard(Card card) {
            this.Cards.Remove(card);
        }

        /// <summary>
        /// Mischt Collection
        /// </summary>
        public void Shuffle() {
            if (this.Cards.Count <= 1) {
                throw new InvalidOperationException("Collection aus 1 oder weniger Elementen! Mischen sinnfrei.");
            }

            var cardList = new List<Card>();
            CardCollection shuffleCollection = new CardCollection();
            Random rand = new Random(DateTime.Now.Ticks.GetHashCode());

            // KartenListe aus Collection
            foreach (Card card in this.Cards) {
                cardList.Add(card);
            }

            // Random aus Kartenliste in SuffleCollection
            do {
                int Index = rand.Next(0, cardList.Count);
                shuffleCollection.AddCard(cardList[Index]);
                cardList.Remove(cardList[Index]);
            } while (cardList.Count > 0);

            this.Cards = shuffleCollection.Cards;
        }
    }
}
