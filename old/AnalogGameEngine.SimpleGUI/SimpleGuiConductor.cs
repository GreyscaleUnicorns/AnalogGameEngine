using System.Linq;

using AnalogGameEngine;
using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI.Entities;

namespace AnalogGameEngine.SimpleGUI {
    public abstract class SimpleGuiConductor<T, U> : Conductor<T, U> where T : ISimpleGuiGame where U : SimpleGuiCardType {
        // TODO: Add stage for texture creation
        public override T StartGame() {
            var game = base.StartGame();
            game.Run(() =>
            {
                foreach (var cardType in this.cardTypeDict.Values) {
                    cardType.LoadTexture();
                }
            });
            return game;
        }
    }
}
