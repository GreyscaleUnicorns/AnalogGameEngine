using System.Collections.Generic;

using CardGameEngine;

namespace CardGameEngine.Entities {
    abstract class CardCollection {
        public LinkedList<Card> Cards { get; private set; }

        public CardCollection(Card[] cards = null) {
            if (cards != null) {
                this.Cards = new LinkedList<Card>(cards);
            } else {
                this.Cards = new LinkedList<Card>();
            }
        }

        public void AddCard(Card card) {
            this.Cards.AddLast(card);
        }
    }
}
