using CardGameEngine.Entities;
using CardGameEngine.Management;

namespace CardGameEngine.Factories {
    public abstract class EffectFactory {
        internal void Initialize(Game game, Registry registry) {
            this.CreateEffects(game, registry);
        }

        protected abstract void CreateEffects(Game game, Registry registry);
    }
}
