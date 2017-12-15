using System;
using System.Collections.Generic;
using Xunit;

using CardGameEngine.Entities;

namespace CardGameEngine.Tests.Entities {
    public class CardCollectionTest {
        [Fact]
        public void ShuffleIntegrityTest() {
            var card1 = new Card("One");
            var card2 = new Card("Two");
            var card3 = new Card("Three");
            var collection = new CardCollection();
            collection.AddCard(card1);
            collection.AddCard(card2);
            collection.AddCard(card3);

            collection.Shuffle();

            var cards = new List<Card>(new Card[] { card1, card2, card3 });
            foreach (Card card in collection.Cards) {
                Assert.Contains(card, cards);
                cards.Remove(card);
            }
            Assert.Empty(cards);
        }
    }
}
