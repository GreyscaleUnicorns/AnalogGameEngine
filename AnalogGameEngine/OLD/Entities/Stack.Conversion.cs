using System;
using System.Linq;

namespace AnalogGameEngine.Entities {
    public partial class Stack<T> {
        /// <summary>
        /// Converts a Stack to a Set.
        /// </summary>
        /// <returns>Set with same cards as this Deck</returns>
        public Set<T> ConvertToSet() => new Set<T>(this.Cards.ToArray());
    }
}
