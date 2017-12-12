using System;

namespace CardGameEngine.Entities
{
    class Deck : CardCollection
    {
        public int OpenCards { get; private set; }

        public Deck(Card[] cards = null) : base(cards) {
            //Nothing to do
        }

        public void AddUnder(Card card) {
            this.Cards.AddFirst(card);
        }

        public Card Draw() {
            if (this.Cards.Count > 1) {
                Card drawn = this.Cards.First.Value;
                this.Cards.RemoveFirst();
                return drawn;
            } else {
                // TODO:
                throw new NotImplementedException();
            }
        }
    }
}
