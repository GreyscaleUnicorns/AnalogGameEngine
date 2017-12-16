using System;
using System.Linq;

namespace CardGameEngine.Entities {
    public partial class Set {
        /// <summary>
        /// Converts a Set to a Stack.
        /// </summary>
        /// <returns>Stack with same cards as this Set</returns>
        public Stack ConvertToDeck() => new Stack(this.Cards.ToArray());
    }

    public partial class Stack {
        /// <summary>
        /// Converts a Stack to a Set.
        /// </summary>
        /// <returns>Set with same cards as this Deck</returns>
        public Set ConvertToSet() => new Set(this.Cards.ToArray());
    }
}
