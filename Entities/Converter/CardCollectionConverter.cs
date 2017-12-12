using System;
using System.Linq;

namespace CardGameEngine.Entities.Converter
{
    static class CardCollectionConverter
    {
        public static Deck ConvertToDeck(this Set set) {
            return new Deck(set.Cards.ToArray());
        }

        public static Set ConvertToSet(this Deck deck) {
            return new Set(deck.Cards.ToArray());
        }
    }
}
