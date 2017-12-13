using System;
using System.Linq;

namespace CardGameEngine.Entities.Converter {
    /// <summary>
    /// Extension methods class to provide conversion methods for CardCollection implementations.
    /// </summary>
    static class CardCollectionConverter {
        /// <summary>
        /// Converts a Set to a Stack.
        /// </summary>
        /// <param name="set">Set to convert</param>
        /// <returns>Stack with same cards as given Set</returns>
        public static Stack ConvertToDeck(this Set set) {
            return new Stack(set.Cards.ToArray());
        }

        /// <summary>
        /// Converts a Stack to a Set.
        /// </summary>
        /// <param name="deck">Stack to convert</param>
        /// <returns>Set with same cards as given Deck</returns>
        public static Set ConvertToSet(this Stack stack) {
            return new Set(stack.Cards.ToArray());
        }
    }
}
