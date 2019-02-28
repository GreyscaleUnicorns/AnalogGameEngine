using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Helper;

namespace AnalogGameEngine.SimpleGUI.Entities {
    public class SimpleGuiCardType : CardType {
        public readonly Texture Texture;

        protected SimpleGuiCardType(Texture texture) : base() {
            this.Texture = texture;
        }
    }
}
