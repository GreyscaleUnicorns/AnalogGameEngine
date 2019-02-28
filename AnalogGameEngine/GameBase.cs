using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using AnalogGameEngine.Entities;

namespace AnalogGameEngine {
    // TODO: for greater flexibility introduce IPlayer and replace Player<T> with IPlayer?
    public abstract class GameBase<T> : CardCollectionHolder<T>, IGameBase where T : ICard {
        private readonly List<Player<T>> players;
        public ImmutableList<Player<T>> Players {
            get {
                return players.ToImmutableList();
            }
        }

        private int activePlayer;
        public Player<T> ActivePlayer {
            get {
                return this.Players[this.activePlayer];
            }
        }

        public virtual Player<T> NextPlayer {
            get {
                return this.Players[(this.activePlayer + 1) % this.Players.Count];
            }
        }

        protected GameBase(Player<T>[] players) {
            if (players is null) { throw new ArgumentNullException("players"); }
            if (players.Length <= 0) { throw new ArgumentException("Player array must not be empty!", "players"); }

            // Initialize
            this.players = new List<Player<T>>();

            // Handle parameters
            foreach (var player in players) {
                this.players.Add(player);
            }
        }

        public void NextTurn() {
            this.activePlayer = this.players.FindIndex(player => player == this.NextPlayer);
        }

        public abstract void StartGame();
    }
}
