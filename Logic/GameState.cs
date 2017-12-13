using System;
using System.Collections.Generic;

using CardGameEngine.Entities;

namespace CardGameEngine.Logic {
    abstract class GameState {
        private Dictionary<string, Set> CommonSets { get; set; }
        private Dictionary<string, Stack> CommonStacks { get; set; }
        public Player[] Players { get; private set; }

        public GameState() {
            this.CommonSets = new Dictionary<string, Set>();
            this.CommonStacks = new Dictionary<string, Stack>();

            Initialize();
        }

        public Set getCommonSet(string id) {
            return this.CommonSets[id];
        }

        public Set getCommonStack(string id) {
            return this.CommonSets[id];
        }

        public void Initialize() {
            this.InitializeSets(this.CommonSets);
            this.InitializeStacks(this.CommonStacks);
        }

        protected virtual void InitializeSets(Dictionary<string, Set> commonSets) {

        }

        protected virtual void InitializeStacks(Dictionary<string, Stack> commonStacks) {

        }
    }
}
