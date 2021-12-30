using System;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Represents a set of cards like hand cards.
    /// </summary>
    public partial class Set<T> : CardCollection<T> where T : ICard {
        public T FirstCard {
            get {
                if (this.Cards.Count > 0) {
                    return this.Cards.First.Value;
                }
                else {
                    return default(T);
                }
            }
        }

        public Set() : this(null) { /* Nothing to do */ }
        public Set(T[] cards) : base(cards) { /* Nothing to do */ }

        override public void AddCard(T card, int position) {
            if (position < 0 || position > this.Cards.Count) {
                this.Cards.AddLast(card);
            }
            else if (position == 0) {
                this.Cards.AddFirst(card);
            }
            else {
                var iterator = this.Cards.First;
                for (int i = 0; i < position; i++) { iterator = iterator.Next; }
                this.Cards.AddBefore(iterator, card);
            }
        }
    }
}
