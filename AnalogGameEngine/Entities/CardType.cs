using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Describes a general GameCard
    /// objects of this class should be unique
    /// </summary>
    public abstract class CardType {
        public List<EffectBase> Effects { get; private set; }

        protected CardType() {
            // Initialize
            this.Effects = new List<EffectBase>();
        }

        public void activateEffects() {
            foreach (var effect in this.Effects) {
                effect.Activate();
            }
        }
    }
}
