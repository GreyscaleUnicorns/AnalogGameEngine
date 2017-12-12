using System;

namespace CardGameEngine.Entities
{
    class Set : CardCollection
    {
        public bool Open { get; private set; }

        public Deck convertToDeck() {
            throw new NotImplementedException();
        }
    }
}
