using System;
using System.Collections.Generic;

namespace AnalogGameEngine.Entities {
    public abstract class CardCollectionBase {
        public event Action OnMoveFrom = delegate { };
        public event Action OnMovedFrom = delegate { };
        public event Action OnMoveTo = delegate { };
        public event Action OnMovedTo = delegate { };

        protected internal void PerformMoveTo() {
            this.OnMoveTo();
        }
        protected internal void PerformMovedTo() {
            this.OnMovedTo();
        }
        protected internal void PerformMoveFrom() {
            this.OnMoveFrom();
        }
        protected internal void PerformMovedFrom() {
            this.OnMovedFrom();
        }

        public void AddCard(ICard card) => this.AddCard(card, 0);
        public abstract void AddCard(ICard card, int position);
        public abstract void RemoveCard(ICard card);
    }
}
