using System.Collections.Immutable;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;

using AnalogGameEngine.SimpleGUI.Factories;

namespace AnalogGameEngine.SimpleGUI.Entities {
    public abstract class SimpleGuiGame : Game {
        private ImmutableDictionary<CardType, string> texturePathDict;

        protected SimpleGuiGame(Player[] players, SimpleGuiCardTypeFactory cardTypeFactory, EffectFactory effectFactory) : base(players, cardTypeFactory, effectFactory) {
            this.texturePathDict = cardTypeFactory.TexturePathDictionary;
        }

        public void Run() {
            using (var window = new GameWindow(this, 800, 600, "MauMauPrototype", texturePathDict)) {
                window.Run(60.0);
            }
        }
    }
}
