using System.Collections.Immutable;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;

using AnalogGameEngine.SimpleGUI.Factories;

namespace AnalogGameEngine.SimpleGUI.Entities {
    public abstract class SimpleGuiGame<T> : Game where T : CardType {
        private ImmutableDictionary<T, string> texturePathDict;

        protected SimpleGuiGame(Player[] players, SimpleGuiCardTypeFactory<T> cardTypeFactory, EffectFactory effectFactory) : base(players, cardTypeFactory, effectFactory) {
            this.texturePathDict = cardTypeFactory.TexturePathDictionary;
        }

        public void Run() {
            using (var window = new GameWindow<T>(this, 800, 600, "MauMauPrototype", texturePathDict)) {
                window.Run(60.0);
            }
        }
    }
}
