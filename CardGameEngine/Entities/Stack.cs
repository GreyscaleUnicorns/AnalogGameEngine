using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// CardCollection, which represents a deck or discard pile
    /// </summary>
    public partial class Stack : CardCollection {
        // TODO: Think about splitting into discard pile and deck

        // TODO: documentation
        /// <summary>
        /// </summary>
        /// <returns>Number of cards in stack</returns>
        public int Count {
            get {
                return this.Cards.Count;
            }
        }

        // TODO: documentation
        /// <summary>
        /// </summary>
        /// <returns>true, if stack is empty - false, otherwise</returns>
        public bool IsEmpty {
            get {
                return this.Cards.Count == 0;
            }
        }

        public Card TopCard {
            get {
                if (this.Cards.Count > 0) {
                    return this.Cards.First.Value;
                } else {
                    return null;
                }
            }
        }

        public Stack() : base() {
            // Nothing to do
        }

        /// <param name="cards"></param>
        public Stack(Card[] cards) : base(cards) {
            // Nothing to do
        }

        override public void AddCard(Card card, int position = 0) {
            if (position < 0 || position > this.Cards.Count)
                this.Cards.AddLast(card);
            else {
                this.Cards.AddFirst(card);
            }
        }
    }
}
