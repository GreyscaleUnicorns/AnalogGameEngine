using System;

using AnalogGameEngine.Entities;

namespace MauMauPrototype {
    public class MauMauPlayer : Player<MauMauCard> {
        public MauMauPlayer(string name) : base(name) {
            // Nothing to do
        }

        protected override string[] GetSetIds() => new string[] { "hand" };

        protected override string[] GetStackIds() => new string[0];
    }
}
