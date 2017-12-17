using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using CardGameEngine.Entities;

namespace CardGameEngine.Entities {
    public abstract class GameState : CardCollectionHolder {
        public LinkedList<Player> Players { get; private set; }
        public Player ActivePlayer { get; set; }

        public GameState() {
            this.Players = new LinkedList<Player>();
        }
    }
}
