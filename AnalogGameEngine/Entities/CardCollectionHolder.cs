using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AnalogGameEngine.Entities
{
    public abstract class CardCollectionHolder
    {
        private Dictionary<string, Set> sets;
        private Dictionary<string, Stack> stacks;

        public ImmutableDictionary<string, Set> Sets
        {
            get
            {
                return sets.ToImmutableDictionary();
            }
        }

        public ImmutableDictionary<string, Stack> Stacks
        {
            get
            {
                return stacks.ToImmutableDictionary();
            }
        }

        internal CardCollectionHolder()
        {
            this.sets = this.CreateDictionary<Set>(this.GetSetIds(), this.CreateSet);
            this.stacks = this.CreateDictionary<Stack>(this.GetStackIds(), this.CreateStack);
        }

        protected abstract string[] GetSetIds();

        protected abstract string[] GetStackIds();

        private Dictionary<string, T> CreateDictionary<T>(string[] ids, Func<T> create)
        {
            var dictionary = new Dictionary<string, T>();
            foreach (string id in ids)
            {
                dictionary.Add(id, create());
            }
            return dictionary;
        }

        private Set CreateSet()
        {
            return new Set();
        }

        private Stack CreateStack()
        {
            return new Stack();
        }
    }
}
