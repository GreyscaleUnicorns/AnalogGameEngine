using System;

using AnalogGameEngine.Entities;
using AnalogGameEngine.SimpleGUI.Helper;
using AnalogGameEngine.SimpleGUI.Entities;

namespace MauMauPrototype {
    public enum Colors { Clubs, Spades, Diamonds, Hearts }
    public enum Values { Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class MauMauCardType : SimpleGuiCardType {
        public Colors Color { get; private set; }
        public Values Value { get; private set; }

        public MauMauCardType(Colors color, Values value, string path) : base(path) {
            this.Color = color;
            this.Value = value;
        }

        public override string ToString() {
            return this.Value.ToString() + " of " + this.Color.ToString();
        }
    }
}
