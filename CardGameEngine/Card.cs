using System;

//blöde Änderung

namespace CardGameEngine {
    /// <summary>
    /// Card Class
    /// implements specific GameCard
    /// which holds unique properties of a card
    /// </summary>
    public class Card {

        private string m_name;
        private int m_id;

        private CardType m_type;

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
    }
}
