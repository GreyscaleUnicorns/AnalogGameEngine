using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI.Factories;
using AnalogGameEngine.Management;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using MauMauPrototype.CardTypes;

namespace MauMauPrototype.Factories {
    class MauMauCardTypeFactory : SimpleGuiCardTypeFactory {
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

        protected override string GetTexturePath(CardType cardType) {
            return "Assets/Textures/Playingcards/01_AceOfHearts.jpg";
        }
    }
}
