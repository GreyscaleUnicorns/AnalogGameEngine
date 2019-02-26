using System;

using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace MauMauPrototype.CardTypes
{
    public enum Colors { Clubs, Spades, Diamonds, Hearts }
    public enum Values { Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    class MauMauCardType : CardType
    {
        public Colors Color { get; private set; }
        public Values Value { get; private set; }

        public MauMauCardType(Registry registry, Colors color, Values value)
          : base(color.ToString().ToLower() + "Of" + value.ToString(), registry)
        {
            this.Color = color;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString() + " of " + this.Color.ToString();
        }
    }
}
