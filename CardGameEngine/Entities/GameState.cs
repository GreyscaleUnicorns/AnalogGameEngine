using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using CardGameEngine.Entities;

namespace CardGameEngine.Entities {
    // ! Discuss modelling
    public abstract class GameState : ICardCollectionHolder {
        private Dictionary<string, Set> sets;
        private Dictionary<string, Stack> stacks;

        // ? Maybe name it just Sets?
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

        public Player[] Players { get; private set; }

        public GameState(int players) {
            if (players <= 0) {
                throw new ArgumentOutOfRangeException("players", "There can only be 1 or more players!");
            }

            this.Players = new Player[players];
            this.sets = this.CreateDictionary<Set>(this.GetCommonSetIds());
            this.stacks = this.CreateDictionary<Stack>(this.GetCommonStackIds());
        }

        protected abstract string[] GetCommonSetIds();

        protected abstract string[] GetCommonStackIds();

        private Dictionary<string, T> CreateDictionary<T>(string[] ids) where T : new() {
            var dictionary = new Dictionary<string, T>();
            foreach (string id in ids) {
                dictionary.Add(id, new T());
            }
            return dictionary;
        }
    }
}
