using System;
using System.Linq;

namespace AnalogGameEngine.Entities {
    public partial class Set<T> {
        /// <summary>
        /// Converts a Set to a Stack.
        /// </summary>
        /// <returns>Stack with same cards as this Set</returns>
        public Stack<T> ConvertToStack() => new Stack<T>(this.Cards.ToArray());
    }
}
