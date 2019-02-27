using System;
using System.Collections.Generic;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace MauMauConsolePrototype.Effects {
    public enum Effects { DrawTwo, Skip }

    abstract class MauMauEffect : Effect {
        public MauMauEffect(string key, Game game, Registry registry) : base(key, game, registry) {
            // Nothing to do
        }
    }
}
