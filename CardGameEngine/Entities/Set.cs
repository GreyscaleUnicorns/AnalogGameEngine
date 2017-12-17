using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Represents a set of cards like hand cards.
    /// </summary>
    public class Set : CardCollection {
        // ? Replace this with some kind of stronger access right system? I'm not sure about this construct...
        public bool Open { get; private set; }

        public Set() : base() {
            // Nothing to do
        }

        public Set(Card[] cards) : base(cards) {
            // Nothing to do
        }

        override public void AddCard(Card card, int position) {
            if (position < 0 || position > this.Cards.Count)
                this.Cards.AddLast(card);
            else {
                var iterator = this.Cards.First;
                for (int i = 0; i < position; i++) iterator = iterator.Next;
                this.Cards.AddBefore(iterator, card);
            }
        }
    }
}
