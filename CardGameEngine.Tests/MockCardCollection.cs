using CardGameEngine.Entities;

namespace CardGameEngine.Tests {
    class MockCardCollection : CardCollection {
        public MockCardCollection(Card[] cards = null) : base(cards) {
            // Nothing to do
        }
    }
}
