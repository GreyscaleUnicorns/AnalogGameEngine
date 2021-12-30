using System;
using System.Collections.Generic;
using System.Linq;

using AnalogGameEngine;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Base for all types of card collections.
    /// </summary>
    public abstract partial class CardCollection<T> : CardCollectionBase where T : ICard {
        /// <summary>
        /// The list of all cards in the collection.
        /// </summary>
        /// <returns>A list of Cards</returns>
        public LinkedList<T> Cards { get; private set; }

        public event Action OnEmpty = delegate { };

        protected CardCollection() : this(null) { /* Nothing to do */ }
        /// <param name="cards">Initial cards in collection</param>
        protected CardCollection(T[] cards) {
            if (cards != null) {
                this.Cards = new LinkedList<T>(cards);
            }
            else {
                this.Cards = new LinkedList<T>();
            }
            this.RegisterEvents();
        }

        protected void RegisterEvents() {
            this.OnMovedFrom += () =>
            {
                // Trigger empty event
                if (this.Cards.Count == 0) {
                    this.OnEmpty();
                }
            };
        }

        public override void AddCard(ICard card, int position) {
            if (card is T) {
                this.AddCard((T)card, position);
            }
            else {
                throw new ArgumentException("Card is not of correct type!");
            }
        }

        public void AddCard(T card) { this.AddCard(card, 0); }
        /// <summary>
        /// Adds a card to the end of the collection.
        /// </summary>
        /// <param name="card">card to append</param>
        /// <param name="position">position at which the card should be inserted</param>
        virtual public void AddCard(T card, int position) {
            // overwrite behaviour in inheriting classes
            this.Cards.AddLast(card);
        }

        public override void RemoveCard(ICard card) {
            if (card is T) {
                this.RemoveCard((T)card);
            }
            else {
                throw new ArgumentException("Card is not of correct type!");
            }
        }

        /// <summary>
        /// Removes a Card from the Collection
        /// </summary>
        /// <param name="card">card to be removed</param>
        public bool RemoveCard(T card) {
            return this.Cards.Remove(card);
        }

        public void MoveAllCardsTo(CardCollectionBase collection) {
            // TODO: add check for move to self
            while (this.Cards.Count > 0) {
                this.Cards.First.Value.moveTo(collection);
            }
        }

        /// <summary>
        /// Shuffles collection
        /// </summary>
        public void Shuffle() {
            if (this.Cards.Count > 1) {
                var cardList = new List<T>();
                LinkedList<T> shuffledList = new LinkedList<T>();
                Random rand = new Random(DateTime.Now.Ticks.GetHashCode());

                // Create list from collection
                foreach (T card in this.Cards) {
                    cardList.Add(card);
                }

                // Randomly move cards from list into new collection
                do {
                    int index = rand.Next(0, cardList.Count);
                    shuffledList.AddLast(cardList[index]);
                    cardList.Remove(cardList[index]);
                } while (cardList.Count > 0);

                this.Cards = shuffledList;
            }
        }
    }
}
