namespace CardGameEngine {
    /// <summary>
    /// Interface for Effects, that can be assigned to cards
    /// </summary>
    public interface IEffect {
        /// <summary>
        /// triggers the execution of this effect
        /// </summary>
        /// <param name="state"> current GameState, that the Effect should be applied on </param>
        void trigger(Logic.GameState state);
    }
}
