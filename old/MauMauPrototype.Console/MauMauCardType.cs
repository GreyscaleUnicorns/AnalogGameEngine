using System;

using AnalogGameEngine.Entities;

namespace MauMauPrototype {
    public enum Colors { Clubs, Spades, Diamonds, Hearts }
    public enum Values { Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class MauMauCardType : CardType {
        public Colors Color { get; private set; }
        public Values Value { get; private set; }

        public MauMauCardType(Colors color, Values value) : base() {
            this.Color = color;
            this.Value = value;
        }

        public override string ToString() {
            return this.Value.ToString() + " of " + this.Color.ToString();
        }
    }
}
