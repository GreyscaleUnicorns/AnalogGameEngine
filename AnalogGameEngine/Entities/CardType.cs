using System;
using System.Collections.Generic;
using System.Linq;

using AnalogGameEngine.Management;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public abstract class CardType {
        protected List<Effect> Effects { get; private set; }

        protected CardType(string key, Registry registry) {
            if (key is null) { throw new ArgumentNullException("key"); }
            if (registry is null) { throw new ArgumentNullException("registry"); }

            // Handle parameters
            registry.RegisterCardType(key, this);

            // Initialize
            this.Effects = new List<Effect>();
        }

        public void activateEffects() {
            foreach (var effect in this.Effects) {
                effect.Activate();
            }
        }
    }
}
