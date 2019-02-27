using System.Collections.Generic;
using System.Collections.Immutable;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.SimpleGUI.Factories {
    public abstract class SimpleGuiCardTypeFactory<T> : CardTypeFactory where T : CardType {
        internal ImmutableDictionary<T, string> TexturePathDictionary { get; private set; }

        protected SimpleGuiCardTypeFactory() {
            this.OnCardTypeCreation += (cardTypes) =>
            {
                // TODO: Error handling

                var dict = new Dictionary<T, string>();
                foreach (T cardType in cardTypes) {
                    dict.Add(cardType, this.GetTexturePath(cardType));
                }
                TexturePathDictionary = dict.ToImmutableDictionary();
            };
        }

        protected abstract string GetTexturePath(T cardType);
    }
}
