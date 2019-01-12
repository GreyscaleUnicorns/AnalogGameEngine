using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;

namespace AnalogGameEngine.Tests {
    class MockGame : Game {
        public MockGame(CardTypeFactory cardTypeFactory, EffectFactory effectFactory) : base(new Player[] { new MockPlayer(), new MockPlayer() }, cardTypeFactory, effectFactory) {
            // Nothing to do
        }

        protected override string[] GetSetIds() {
            return new string[0];
        }

        protected override string[] GetStackIds() {
            return new string[0];
        }
    }
}
