using AnalogGameEngine.Entities;

namespace AnalogGameEngine.Tests {
    class MockCardCollection : CardCollection<MockCard> {
        public MockCardCollection(MockCard[] cards = null) : base(cards) {
            // Nothing to do
        }
    }
}
