using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using AnalogGameEngine.Factories;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.Entities
{
    public abstract class Game : CardCollectionHolder
    {
        protected Registry registry;

        private readonly List<Player> players;
        public ImmutableList<Player> Players
        {
            get
            {
                return players.ToImmutableList();
            }
        }

        private int activePlayer;
        public Player ActivePlayer
        {
            get
            {
                return this.Players[this.activePlayer];
            }
        }

        public virtual Player NextPlayer
        {
            get
            {
                return this.Players[(this.activePlayer + 1) % this.Players.Count];
            }
        }

        protected Game(Player[] players, CardTypeFactory cardTypeFactory, EffectFactory effectFactory)
        {
            if (players is null) { throw new ArgumentNullException("players"); }
            if (players.Length <= 0) { throw new ArgumentException("Player array must not be empty!", "players"); }
            if (cardTypeFactory is null) { throw new ArgumentNullException("cardTypeFactory"); }
            if (effectFactory is null) { throw new ArgumentNullException("effectFactory"); }

            // Initialize
            this.registry = new Registry();
            this.players = new List<Player>();

            // Handle parameters
            foreach (var player in players)
            {
                this.players.Add(player);
            }
            effectFactory.Initialize(this, registry);
            cardTypeFactory.Initialize(registry);
        }

        public void NextTurn()
        {
            this.activePlayer = this.players.FindIndex(player => player == this.NextPlayer);
        }
    }
}
