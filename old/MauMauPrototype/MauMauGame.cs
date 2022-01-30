using System;
using System.Collections.Generic;

using AnalogGameEngine;
using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI;

namespace MauMauPrototype {
    public class MauMauGame : SimpleGuiGame<MauMauCard, MauMauCardType> {
        public MauMauGame(MauMauPlayer[] players) : base(players) { }

        public override void StartGame() {
            // Register events
            this.Stacks["deck"].OnEmpty += () =>
            {
                while (this.Stacks["discard-pile"].Cards.Count > 1) {
                    this.Stacks["discard-pile"].Cards.First.Next.Value.moveTo(this.Stacks["deck"]);
                }
                this.Stacks["deck"].Shuffle();
            };

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
