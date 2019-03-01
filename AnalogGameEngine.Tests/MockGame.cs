using AnalogGameEngine.Entities;

namespace AnalogGameEngine.Tests {
    class MockGame : GameBase<MockCard> {
        public MockGame() : base(new MockPlayer[] { new MockPlayer(), new MockPlayer() }) {
            // Nothing to do
        }

        public override void StartGame() {
            // Nothing to do
        }

        protected override string[] GetSetIds() => new string[0];

        protected override string[] GetStackIds() => new string[0];
    }
}
