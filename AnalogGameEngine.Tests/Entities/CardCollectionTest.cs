using System;
using System.Collections.Generic;
using Xunit;

using AnalogGameEngine.Entities;

namespace AnalogGameEngine.Tests.Entities {
    public class CardCollectionTest {
        [Fact]
        public void ShuffleIntegrityTest() {
            var cardType = new MockCardType();
            var collection = new MockCardCollection();
            var card1 = new MockCard(cardType, collection);
            var card2 = new MockCard(cardType, collection);
            var card3 = new MockCard(cardType, collection);

            collection.Shuffle();

            var cards = new List<MockCard>(new[] { card1, card2, card3 });
            foreach (MockCard card in collection.Cards) {
                Assert.Contains(card, cards);
                cards.Remove(card);
            }
            Assert.Empty(cards);
        }

        /// <summary>
        /// Testet die Mischfunktion auf Mischergebnis/ Unterschied zur ungemischten Liste
        /// </summary>
        [Fact]
        public void ShuffleTest() {
            var cardType = new MockCardType();
            var collection = new MockCardCollection();
            var card1 = new MockCard(cardType, collection);
            var card2 = new MockCard(cardType, collection);
            var card3 = new MockCard(cardType, collection);
            var card4 = new MockCard(cardType, collection);
            var card5 = new MockCard(cardType, collection);
            var cards = new List<MockCard>(new[] { card1, card2, card3, card4, card5 });
            var cardsTest = new List<MockCard>();
            int differents = 0; // Anzahl Unterschiede zu Original Liste
            double mischGrad = 0.5; // Bestimmen wie viel Karten unterschiedlich sein sollen
            int minDiff = Convert.ToInt32(Math.Round(cards.Count * mischGrad));

            collection.AddCard(card1);
            collection.AddCard(card2);
            collection.AddCard(card3);
            collection.AddCard(card4);
            collection.AddCard(card5);

            collection.Shuffle();

            // Karten Liste der gemischten Collection
            foreach (MockCard card in collection.Cards) {
                cardsTest.Add(card);
            }

            // Gemischte Karten Liste und Original Liste vergleichen
            for (int i = 0; i < cards.Count; i++) {
                if (cards[i] != cardsTest[i]) {
                    differents += 1;
                }
            }
            Assert.True(differents >= minDiff);
        }
    }
}
