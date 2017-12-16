using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CardGameEngine.Entities {
    public class Player {
        public string Name { get; private set; }

        private Dictionary<string, Set> sets;
        public ImmutableDictionary<string, Set> Sets {
            get {
                return sets.ToImmutableDictionary();
            }
        }

        private Dictionary<string, Stack> stacks;
        public ImmutableDictionary<string, Stack> Stacks {
            get {
                return stacks.ToImmutableDictionary();
            }
        }

        public Player() {
            this.sets = new Dictionary<string, Set>();
            this.stacks = new Dictionary<string, Stack>();
        }
    }
}
