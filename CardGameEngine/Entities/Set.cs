using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Represents a set of cards like hand cards.
    /// </summary>
    public partial class Set : CardCollection {
        // ? Replace this with some kind of stronger access right system? I'm not sure about this construct...
        public bool Open { get; private set; }

        public Set() : base() {
            // Nothing to do
        }

        public Set(Card[] cards) : base(cards) {
            // Nothing to do
        }
    }
}
