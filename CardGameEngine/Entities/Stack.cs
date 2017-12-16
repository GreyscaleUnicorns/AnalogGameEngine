using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// CardCollection, which represents a deck or discard pile
    /// </summary>
    public partial class Stack : CardCollection {
        // TODO: Think about splitting into discard pile and deck

        // TODO: documentation
        /// <summary>
        /// </summary>
        /// <returns>Number of cards in stack</returns>
        public int Count {
            get {
                return this.Cards.Count;
            }
        }

        // TODO: documentation
        /// <summary>
        /// </summary>
        /// <returns>true, if stack is empty - false, otherwise</returns>
        public bool IsEmpty {
            get {
                return this.Cards.Count == 0;
            }
        }

        // ? Wouldn't a boolean to determine, if first card is visible, suffice?
        public int OpenCards { get; private set; }

        public Stack() : base() {
            // Nothing to do
        }

        /// <param name="cards"></param>
        public Stack(Card[] cards) : base(cards) {
            // Nothing to do
        }

        /// <summary>
        /// Adds a card to the top of the stack.
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddToTop(Card card) {
            this.Cards.AddFirst(card);
        }

        /// <summary>
        /// Add a card to the bottom of the stack.
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddToBottom(Card card) {
            this.Cards.AddLast(card);
        }

        /// <summary>
        /// Draws the first card from the stack.
        /// </summary>
        /// <remarks>
        /// The drawn card will be removed from the stack.
        /// </remarks>
        /// <returns>Drawn card or null, if stack is empty</returns>
        public Card Draw() {
            // ? This could be a Deck specific function, think about split mentioned above
            if (this.Cards.Count > 1) {
                Card drawn = this.Cards.First.Value;
                this.Cards.RemoveFirst();
                return drawn;
            } else {
                return null;
            }
        }
    }
}
