using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Represents a set of cards like hand cards.
    /// </summary>
    class Set : CardCollection {
        // ? Replace this with some kind of stronger access right system? I'm not sure about this construct...
        public bool Open { get; private set; }

        public Set(Card[] cards = null) : base(cards) {
            //Nothing to do
        }
    }
}
