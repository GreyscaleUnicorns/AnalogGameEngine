using System;
using System.Collections.Generic;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace MauMauConsolePrototype.Effects {
    class DrawTwoEffect : MauMauEffect {
        public DrawTwoEffect(string key, Game game, Registry registry) : base(key, game, registry) {
            // Nothing to do
        }

        protected override void Trigger(Game game) {
            var nextPlayer = game.NextPlayer;
            for (int i = 0; i < 2; i++) {
                if (game.Stacks["deck"].TopCard != null) {
                    game.Stacks["deck"].TopCard.moveTo(nextPlayer.Sets["hand"]);
                }
            }
        }
    }
}
