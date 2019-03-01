using System;
using System.Collections.Generic;

using AnalogGameEngine.Entities;

namespace MauMauPrototype.Effects {
    class DrawTwoEffect : MauMauEffect {
        protected override void Trigger(MauMauGame game) {
            var nextPlayer = game.NextPlayer;
            for (int i = 0; i < 2; i++) {
                if (game.Stacks["deck"].TopCard != null) {
                    game.Stacks["deck"].TopCard.moveTo(nextPlayer.Sets["hand"]);
                }
            }
        }
    }
}
