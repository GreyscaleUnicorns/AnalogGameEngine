using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using AnalogGameEngine;
using AnalogGameEngine.Entities;

using MauMauPrototype.Effects;

namespace MauMauPrototype {
    public class MauMauConductor : Conductor<MauMauGame, MauMauCardType> {
        protected override (string, Effect<MauMauGame>)[] CreateEffects() {
            var effects = new List<(string, Effect<MauMauGame>)>();
            effects.Add(("drawTwo", new DrawTwoEffect()));
            effects.Add(("skip", new SkipEffect()));
            return effects.ToArray();
        }

        protected override (string, MauMauCardType)[] CreateCardTypes(ImmutableDictionary<string, Effect<MauMauGame>> effects) {
            var cardTypes = new List<(string, MauMauCardType)>();
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                foreach (Values value in Enum.GetValues(typeof(Values))) {
                    var cardType = new MauMauCardType(color, value);
                    switch (value) {
                        case Values.Seven:
                            cardType.Effects.Add(effects["drawTwo"]);
                            break;
                        case Values.Eight:
                            cardType.Effects.Add(effects["skip"]);
                            break;
                    }
                    cardTypes.Add((color.ToString().ToLower() + "Of" + value.ToString(), cardType));
                }
            }
            return cardTypes.ToArray();
        }

        protected override MauMauGame CreateGame(ImmutableDictionary<string, MauMauCardType> cardTypes) {
            // Create players
            MauMauPlayer[] players = new MauMauPlayer[] {
                new MauMauPlayer("Peter"),
                new MauMauPlayer("Hans"),
                new MauMauPlayer("Friedrich")
            };
            var game = new MauMauGame(players);

            // Fill deck with cards
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                foreach (Values value in Enum.GetValues(typeof(Values))) {
                    new MauMauCard(cardTypes[color.ToString().ToLower() + "Of" + value.ToString()], game.Stacks["deck"]);
                    new MauMauCard(cardTypes[color.ToString().ToLower() + "Of" + value.ToString()], game.Stacks["deck"]);
                }
            }

            return game;
        }
    }
}
