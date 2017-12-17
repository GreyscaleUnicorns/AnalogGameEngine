using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Represents a set of cards like hand cards.
    /// </summary>
    public partial class Set : CardCollection {
        public Set() : base() {
            // Nothing to do
        }

        public Set(Card[] cards) : base(cards) {
            // Nothing to do
        }
    }
}
