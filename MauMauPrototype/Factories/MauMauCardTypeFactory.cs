using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI.Factories;
using AnalogGameEngine.Management;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

using MauMauPrototype.CardTypes;

namespace MauMauPrototype.Factories {
    class MauMauCardTypeFactory : SimpleGuiCardTypeFactory<MauMauCardType> {
        protected override void CreateCardTypes(Registry registry) {
            foreach (Colors color in Enum.GetValues(typeof(Colors))) {
                foreach (Values value in Enum.GetValues(typeof(Values))) {
                    switch (value) {
                        case Values.Seven:
                            new Seven(registry, color);
                            break;
                        case Values.Eight:
                            new Eight(registry, color);
                            break;
                        default:
                            new MauMauCardType(registry, color, value);
                            break;
                    }
                }
            }
        }

        protected override string GetTexturePath(MauMauCardType cardType) {
            int number = 0;
            string basePath = "Assets/Textures/Playingcards";
            string fileName = "";
            switch (cardType.Value) {
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
            switch (cardType.Color) {
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
    }
}
