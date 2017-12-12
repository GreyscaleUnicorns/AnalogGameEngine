using System.Collections.Generic;

using CardGameEngine;

namespace CardGameEngine.Entities {
    abstract class CardCollection {
        public LinkedList<Card> Cards { get; private set; }

        public CardCollection() {
            this.Cards = new LinkedList<Card>();
        }

        public void AddCard(Card card) {
            this.Cards.AddLast(card);
        }
    }
}
