using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AnalogGameEngine.Entities {
    public abstract class Player<T> : CardCollectionHolder<T> where T : ICard {
        public string Name { get; private set; }

        protected Player(string name) {
            this.Name = name;
        }
    }
}
