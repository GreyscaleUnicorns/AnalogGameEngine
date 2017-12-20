namespace CardGameEngine.Entities {
    /// <summary>
    /// Class for Effects, that can be assigned to cards
    /// </summary>
    public abstract class Effect : SelfRegistry<Effect> {
        public Effect(string name) : base(name) {
            // Nothing to do
        }

        /// <summary>
        /// triggers the execution of this effect
        /// </summary>
        /// <param name="state"> current GameState, that the Effect should be applied on </param>
        public abstract void trigger(Entities.GameState state);
    }
}
