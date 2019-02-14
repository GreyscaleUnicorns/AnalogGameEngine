using System;
using System.Collections.Generic;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace MauMauPrototype.Effects {
    class SkipEffect : MauMauEffect {
        public SkipEffect(string key, Game game, Registry registry) : base(key, game, registry) {
            // Nothing to do
        }

        protected override void Trigger(Game game) {
            game.NextTurn();
        }
    }
}
