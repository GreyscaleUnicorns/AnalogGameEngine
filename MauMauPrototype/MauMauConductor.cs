using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

using AnalogGameEngine;
using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI.Helper;

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
                    var path = GetTexturePath(color, value);
                    var cardType = new MauMauCardType(color, value, path);
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

        private static string GetTexturePath(Colors color, Values value) {
            int number = 0;
            string basePath = "Assets/Textures/Playingcards";
            string fileName = "";
            switch (value) {
                case Values.Ace:
                    number += 1;
                    fileName += "Ace";
                    break;
                case Values.Seven:
                    number += 7;
                    fileName += "Seven";
                    break;
                case Values.Eight:
                    number += 8;
                    fileName += "Eight";
                    break;
                case Values.Nine:
                    number += 9;
                    fileName += "Nine";
                    break;
                case Values.Ten:
                    number += 10;
                    fileName += "Ten";
                    break;
                case Values.Jack:
                    number += 11;
                    fileName += "Jack";
                    break;
                case Values.Queen:
                    number += 12;
                    fileName += "Queen";
                    break;
                case Values.King:
                    number += 13;
                    fileName += "King";
                    break;
            }
            switch (color) {
                case Colors.Hearts:
                    fileName += "OfHearts";
                    break;
                case Colors.Diamonds:
                    fileName += "OfDiamonds";
                    number += 13;
                    break;
                case Colors.Clubs:
                    fileName += "OfClubs";
                    number += 26;
                    break;
                case Colors.Spades:
                    fileName += "OfSpades";
                    number += 39;
                    break;
            }
            fileName = number + "_" + fileName + ".jpg";
            if (number < 10) {
                fileName = "0" + fileName;
            }
            return Path.Combine(basePath, fileName);
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
