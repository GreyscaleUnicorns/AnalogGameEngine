using System;
using System.Collections.Generic;

using CardGameEngine.Entities;

namespace CardGameEngine.Logic {
    public abstract class GameState {
        private Dictionary<string, Set> CommonSets { get; set; }
        private Dictionary<string, Stack> CommonStacks { get; set; }
        public Player[] Players { get; private set; }

        public GameState(int players) {
            if (players <= 0) {
                throw new ArgumentOutOfRangeException("players", "There can only be 1 or more players!");
            }

            this.Players = new Player[players];
            this.CommonSets = this.CreateDictionary<Set>(this.GetCommonSetIds());
            this.CommonStacks = this.CreateDictionary<Stack>(this.GetCommonStackIds());
        }

        public Set getCommonSet(string id) {
            return this.CommonSets[id];
        }

        public Set getCommonStack(string id) {
            return this.CommonSets[id];
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
