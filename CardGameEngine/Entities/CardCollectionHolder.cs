using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CardGameEngine.Entities {
    public abstract class CardCollectionHolder {
        private Dictionary<string, Set> sets;
        private Dictionary<string, Stack> stacks;

        public ImmutableDictionary<string, Set> Sets {
            get {
                return sets.ToImmutableDictionary();
            }
        }

        public ImmutableDictionary<string, Stack> Stacks {
            get {
                return stacks.ToImmutableDictionary();
            }
        }

        public CardCollectionHolder() {
            this.sets = this.CreateDictionary<Set>(this.GetSetIds());
            this.stacks = this.CreateDictionary<Stack>(this.GetStackIds());
        }

        protected abstract string[] GetSetIds();

        protected abstract string[] GetStackIds();

        private Dictionary<string, T> CreateDictionary<T>(string[] ids) where T : new() {
            var dictionary = new Dictionary<string, T>();
            foreach (string id in ids) {
                dictionary.Add(id, new T());
            }
            return dictionary;
        }
    }
}
