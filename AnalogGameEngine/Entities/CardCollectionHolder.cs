using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AnalogGameEngine.Entities {
    public abstract class CardCollectionHolder {
        private readonly Dictionary<string, Set> sets;
        private readonly Dictionary<string, Stack> stacks;

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

        internal CardCollectionHolder() {
            this.sets = CreateDictionary<Set>(this.GetSetIds(), CreateSet);
            this.stacks = CreateDictionary<Stack>(this.GetStackIds(), CreateStack);
        }

        protected abstract string[] GetSetIds();

        protected abstract string[] GetStackIds();

        private static Dictionary<string, T> CreateDictionary<T>(string[] ids, Func<T> create) {
            var dictionary = new Dictionary<string, T>();
            foreach (string id in ids) {
                dictionary.Add(id, create());
            }
            return dictionary;
        }

        private static Set CreateSet() {
            return new Set();
        }

        private static Stack CreateStack() {
            return new Stack();
        }
    }
}
