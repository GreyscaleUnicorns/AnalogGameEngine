using System.Collections.Immutable;

using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Entities;

namespace AnalogGameEngine.SimpleGUI {
    public abstract class SimpleGuiGame<T, U> : GameBase<T> where T : SimpleGuiCard<U> where U : SimpleGuiCardType {
        protected SimpleGuiGame(Player<T>[] players) : base(players) { }

        public void Run() {
            using (var window = new GameWindow<T, U>(this, 800, 600, "MauMauPrototype")) {
                window.Run(60.0);
            }
        }
    }
}
