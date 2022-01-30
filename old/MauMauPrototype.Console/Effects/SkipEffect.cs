using System;
using System.Collections.Generic;

using AnalogGameEngine.Entities;

namespace MauMauPrototype.Effects {
    class SkipEffect : MauMauEffect {
        protected override void Trigger(MauMauGame game) {
            game.NextTurn();
        }
    }
}
