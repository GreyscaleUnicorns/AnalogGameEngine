using AnalogGameEngine.Entities;

namespace AnalogGameEngine.Tests {
    class MockCardCollection : CardCollection {
        public MockCardCollection(Card[] cards = null) : base(cards) {
            // Nothing to do
        }
    }
}
