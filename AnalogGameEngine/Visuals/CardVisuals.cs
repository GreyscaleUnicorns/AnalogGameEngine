using System;

namespace AnalogGameEngine.Visuals
{
    public class CardVisuals
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public CardVisuals(int width, int height) {
            this.Width = width;
            this.Height = height;
        }
    }
}
