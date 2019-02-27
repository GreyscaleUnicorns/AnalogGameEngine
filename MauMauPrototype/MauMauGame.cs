using AnalogGameEngine.Entities;
using System;
using System.Collections.Generic;

using MauMauPrototype.CardTypes;
using MauMauPrototype.Factories;

namespace MauMauPrototype {
    class MauMauGame : Game {
        public MauMauGame(Player[] players) : base(players, new MauMauCardTypeFactory(), new MauMauEffectFactory()) {
            // Register events
            this.Stacks["deck"].OnEmpty += () =>
            {
                while (this.Stacks["discard-pile"].Cards.Count > 1) {
                    this.Stacks["discard-pile"].Cards.First.Next.Value.moveTo(this.Stacks["deck"]);
                }
                this.Stacks["deck"].Shuffle();
            };

            // Fill deck with cards
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                foreach (Values value in Enum.GetValues(typeof(Values))) {
                    new Card(registry.GetCardType(color.ToString().ToLower() + "Of" + value.ToString()), this.Stacks["deck"]);
                }
            }

            this.Stacks["deck"].Shuffle();

            // Give out cards to players
            foreach (var player in this.Players) {
                for (var i = 0; i < 6; i++) {
                    this.Stacks["deck"].TopCard.moveTo(player.Sets["hand"]);
                }
            }

            this.Stacks["deck"].TopCard.moveTo(this.Stacks["discard-pile"]);
        }

        protected override string[] GetSetIds() => new string[0];

        protected override string[] GetStackIds() => new string[] { "deck", "discard-pile" };
    }
}
