using System;

namespace AnalogGameEngine.Entities
{
    /// <summary>
    /// Card Class
    /// implements specific GameCard
    /// which holds unique properties of a card
    /// </summary>
    public class Card
    {
        public CardType Type { get; private set; }

        private CardCollection collection;

        /// <summary>
        /// Generates a new Card with a given type
        /// </summary>
        /// <param name="name">name of the new card</param>
        /// <param name="type">type of the new card</param>
        public Card(CardType type, CardCollection collection)
        {
            Init(type, collection);
        }

        // Initializer
        private void Init(CardType type, CardCollection collection)
        {
            if (type is null) { throw new ArgumentNullException("type"); }
            if (collection is null) { throw new ArgumentNullException("collection"); }

            this.Type = type;
            this.moveTo(collection);
        }

        public void moveTo(Entities.CardCollection collection) { this.moveTo(collection, 0); }

        /// <summary>
        /// moves the current Card to the specified collection
        /// </summary>
        /// <param name="collection">the targeted collection</param>
        /// <param name="position">position at which the card should be inserted</param>
        public void moveTo(Entities.CardCollection collection, int position)
        {
            if (this.collection != null)
            {
                this.collection.PerformMoveFrom();
                this.collection.RemoveCard(this);
                this.collection.PerformMovedFrom();
            }
            collection.PerformMoveTo();
            this.collection = collection;
            this.collection.AddCard(this, position);
            this.collection.PerformMovedTo();
        }

        public void activateEffects()
        {
            this.Type.activateEffects();
        }
    }
}
