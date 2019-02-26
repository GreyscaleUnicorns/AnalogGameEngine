using System;
using System.Linq;

namespace AnalogGameEngine.Entities
{
    public partial class Set
    {
        /// <summary>
        /// Converts a Set to a Stack.
        /// </summary>
        /// <returns>Stack with same cards as this Set</returns>
        public Stack ConvertToStack() => new Stack(this.Cards.ToArray());
    }
}
