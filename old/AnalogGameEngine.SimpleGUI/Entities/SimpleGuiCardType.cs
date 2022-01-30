using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Helper;

namespace AnalogGameEngine.SimpleGUI.Entities {
    public class SimpleGuiCardType : CardType {
        private readonly string path;
        public Texture Texture { get; private set; }

        protected SimpleGuiCardType(string path) : base() {
            this.path = path;
        }

        // TODO: workaround for now
        public void LoadTexture() {
            this.Texture = new Texture(path);
        }
    }
}
