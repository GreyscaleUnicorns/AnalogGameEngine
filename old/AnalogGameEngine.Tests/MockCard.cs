using System.Linq;

using AnalogGameEngine.Entities;

namespace AnalogGameEngine.Tests {
    public class MockCard : Card<MockCardType> {
        public MockCard(MockCardType type, CardCollection<MockCard> collection) : base(type, collection) { }
    }
}
