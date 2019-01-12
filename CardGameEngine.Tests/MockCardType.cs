using CardGameEngine.Management;

namespace CardGameEngine.Entities {
    class MockCardType : CardType {
        public MockCardType(Registry registry) : base("mock", registry) {
            // Nothing to do
        }
    }
}
