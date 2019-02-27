using AnalogGameEngine.Management;

namespace AnalogGameEngine.Entities {
    class MockCardType : CardType {
        public MockCardType(Registry registry) : base("mock", registry) {
            // Nothing to do
        }
    }
}
