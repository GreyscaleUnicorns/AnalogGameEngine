using AnalogGameEngine.Management;

namespace AnalogGameEngine.Entities
{
    /// <summary>
    /// Class for Effects, that can be assigned to cards
    /// </summary>
    public abstract class Effect
    {
        private readonly Game game;

        protected Effect(string key, Game game, Registry registry)
        {
            registry.RegisterEffect(key, this);
            this.game = game;
        }

        internal void Activate()
        {
            this.Trigger(this.game);
        }

        /// <summary>
        /// triggers the execution of this effect
        /// </summary>
        protected abstract void Trigger(Game game);
    }
}
