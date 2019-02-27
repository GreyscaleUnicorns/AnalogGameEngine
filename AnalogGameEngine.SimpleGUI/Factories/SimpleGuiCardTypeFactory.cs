using System.Collections.Generic;
using System.Collections.Immutable;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.SimpleGUI.Factories {
    public abstract class SimpleGuiCardTypeFactory : CardTypeFactory {
        internal ImmutableDictionary<CardType, string> TexturePathDictionary { get; private set; }

        protected SimpleGuiCardTypeFactory() {
            this.OnCardTypeCreation += (cardTypes) =>
            {
                var dict = new Dictionary<CardType, string>();
                foreach (var cardType in cardTypes) {
                    dict.Add(cardType, this.GetTexturePath(cardType));
                }
                TexturePathDictionary = dict.ToImmutableDictionary();
            };
        }

        protected abstract string GetTexturePath(CardType cardType);
    }
}
