using AnalogGameEngine;

namespace AnalogGameEngine.Entities {
    /// <summary>
    /// Class for Effects, that can be assigned to cards
    /// </summary>
    public abstract class Effect<T> : EffectBase where T : IGameBase {
        private T game;

        protected Effect() { }

        internal void InjectGame(T game) {
            this.game = game;
        }

        internal override void Activate() {
            this.Trigger(this.game);
        }

        /// <summary>
        /// triggers the execution of this effect
        /// </summary>
        protected abstract void Trigger(T game);
    }
}
