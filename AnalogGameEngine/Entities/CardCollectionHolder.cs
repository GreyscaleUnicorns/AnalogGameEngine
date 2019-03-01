using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AnalogGameEngine.Entities {
    public abstract class CardCollectionHolder<T> where T : ICard {
        private readonly Dictionary<string, Set<T>> sets;
        private readonly Dictionary<string, Stack<T>> stacks;

        public ImmutableDictionary<string, Set<T>> Sets {
            get {
                return sets.ToImmutableDictionary();
            }
        }

        public ImmutableDictionary<string, Stack<T>> Stacks {
            get {
                return stacks.ToImmutableDictionary();
            }
        }

        internal CardCollectionHolder() {
            this.sets = CreateDictionary<Set<T>>(this.GetSetIds(), CreateSet);
            this.stacks = CreateDictionary<Stack<T>>(this.GetStackIds(), CreateStack);
        }

        protected abstract string[] GetSetIds();

        protected abstract string[] GetStackIds();

        private static Dictionary<string, S> CreateDictionary<S>(string[] ids, Func<S> create) {
            var dictionary = new Dictionary<string, S>();
            foreach (string id in ids) {
                dictionary.Add(id, create());
            }
            return dictionary;
        }

        private static Set<T> CreateSet() {
            return new Set<T>();
        }

        private static Stack<T> CreateStack() {
            return new Stack<T>();
        }
    }
}
