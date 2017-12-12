using System;

namespace CardGameEngine.Entities
{
    class Set : CardCollection
    {
        public bool Open { get; private set; }

        public Set(Card[] cards = null) : base(cards) {
            //Nothing to do
        }
    }
}
