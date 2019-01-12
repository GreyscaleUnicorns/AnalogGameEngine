using System;
using System.Collections.Generic;

using AnalogGameEngine;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Base for all types of card collections.
    /// </summary>
    public abstract partial class CardCollection {
        /// <summary>
        /// The list of all cards in the collection.
        /// </summary>
        /// <returns>A list of Cards</returns>
        public LinkedList<Card> Cards { get; private set; }

        /// <param name="cards">Initial cards in collection</param>
        public CardCollection(Card[] cards = null) {
            if (cards != null) {
                this.Cards = new LinkedList<Card>(cards);
            }
            else {
                this.Cards = new LinkedList<Card>();
            }
            this.RegisterEvents();
        }

        /// <summary>
        /// Adds a card to the end of the collection.
        /// </summary>
        /// <param name="card">the card to append</param>
        /// <param name="position">the position at which the card should be inserted</param>
        virtual public void AddCard(Card card, int position = 0) {
            // overwirte behaviour in inheriting classes
            this.Cards.AddLast(card);
        }

        /// <summary>
        /// removes a Card from the Collection
        /// </summary>
        /// <param name="card">card to be removed</param>
        public bool RemoveCard(Card card) {
            return this.Cards.Remove(card);
        }

        public void MoveAllCardsTo(CardCollection collection) {
            if (collection != this) {
                while (this.Cards.Count > 0) {
                    this.Cards.First.Value.moveTo(collection);
                }
            }
        }

        /// <summary>
        /// Mischt Collection
        /// </summary>
        public void Shuffle() {
            if (this.Cards.Count <= 1) {
                throw new InvalidOperationException("Collection aus 1 oder weniger Elementen! Mischen sinnfrei.");
            }

            var cardList = new List<Card>();
            LinkedList<Card> shuffleList = new LinkedList<Card>();
            Random rand = new Random(DateTime.Now.Ticks.GetHashCode());

            // KartenListe aus Collection
            foreach (Card card in this.Cards) {
                cardList.Add(card);
            }

            // Random aus Kartenliste in SuffleCollection
            do {
                int Index = rand.Next(0, cardList.Count);
                shuffleList.AddLast(cardList[Index]);
                cardList.Remove(cardList[Index]);
            } while (cardList.Count > 0);

            this.Cards = shuffleList;
        }
    }
}
