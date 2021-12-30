using System;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// CardCollection, which represents a deck or discard pile
    /// </summary>
    public partial class Stack<T> : CardCollection<T> where T : ICard {
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

        public T TopCard {
            get {
                if (this.Cards.Count > 0) {
                    return this.Cards.First.Value;
                }
                else {
                    return default(T);
                }
            }
        }

        public Stack() : this(null) { /* Nothing to do */ }
        public Stack(T[] cards) : base(cards) { /* Nothing to do */ }

        override public void AddCard(T card, int position) {
            if (position < 0 || position > this.Cards.Count) {
                this.Cards.AddLast(card);
            }
            else {
                this.Cards.AddFirst(card);
            }
        }
    }
}
