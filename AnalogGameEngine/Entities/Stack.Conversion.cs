using System;
using System.Linq;

namespace AnalogGameEngine.Entities
{
    public partial class Stack
    {
        /// <summary>
        /// Converts a Stack to a Set.
        /// </summary>
        /// <returns>Set with same cards as this Deck</returns>
        public Set ConvertToSet() => new Set(this.Cards.ToArray());
    }
}
