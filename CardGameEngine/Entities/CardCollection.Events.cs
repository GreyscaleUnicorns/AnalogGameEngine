using System;

namespace CardGameEngine.Entities {
    /// <summary>
    /// Base for all types of card collections.
    /// </summary>
    public partial class CardCollection {
        public event Action OnMoveFrom = delegate { };
        public event Action OnMovedFrom = delegate { };
        public event Action OnMoveTo = delegate { };
        public event Action OnMovedTo = delegate { };
        public event Action OnEmpty = delegate { };

        internal void PerformMoveTo() {
            this.OnMoveTo();
        }
        internal void PerformMovedTo() {
            this.OnMovedTo();
        }
        internal void PerformMoveFrom() {
            this.OnMoveFrom();
        }
        internal void PerformMovedFrom() {
            this.OnMovedFrom();
        }

        protected void RegisterEvents() {
            this.OnMovedFrom += () =>
            {
                // Trigger empty event
                if (this.Cards.Count == 0) {
                    this.OnEmpty();
                }
            };
        }
    }
}
