using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.Factories {
    public abstract class EffectFactory {
        internal void Initialize(Game game, Registry registry) {
            this.CreateEffects(game, registry);
        }

        protected abstract void CreateEffects(Game game, Registry registry);
    }
}
