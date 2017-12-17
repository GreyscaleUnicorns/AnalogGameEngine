using System;

using CardGameEngine.Entities;

namespace CardGameEngine {
    /// <summary>
    /// Card Class
    /// implements specific GameCard
    /// which holds unique properties of a card
    /// </summary>
    public class Card {

        #region Members
        private string m_name;
        private int m_id;

        private CardType m_type;
        private CardCollection cardCollection;
        #endregion

        #region Constructors
        /// <summary>
        /// Generates a new Card
        /// </summary>
        /// <param name="name">name of the new Card</param>
        public Card(string name) {
            Init(name, CardType.Get("default"));
        }

        public Card(string name, CardType type) {
            Init(name, type);
        }

        // initilizer
        private void Init(string name, CardType type) {
            m_name = name;
            m_type = type;
        }
        #endregion

        public void moveTo(CardCollection collection, int index = 0) {
            // TODO: implement index functionality
            this.cardCollection.RemoveCard(this);
            collection.AddCard(this);
            this.cardCollection = collection;
        }
    }
}
