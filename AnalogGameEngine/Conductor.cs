using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using AnalogGameEngine.Entities;

namespace AnalogGameEngine {
    public abstract class Conductor<T, U> where T : IGameBase where U : CardType {
        public T StartGame() {
            var effects = this.CreateEffects();
            var effectDict = ToImmutableDictionary(effects);
            var cardTypeDict = ToImmutableDictionary(this.CreateCardTypes(effectDict));
            var game = this.CreateGame(cardTypeDict);
            // Inject game into effects
            foreach (var (key, effect) in effects) {
                effect.InjectGame(game);
            }
            game.StartGame();
            return game;
        }

        // TODO: Think about enums/generics instead of strings as keys?
        protected abstract (string, Effect<T>)[] CreateEffects();
        protected abstract (string, U)[] CreateCardTypes(ImmutableDictionary<string, Effect<T>> effects);
        protected abstract T CreateGame(ImmutableDictionary<string, U> cardTypes);

        protected static ImmutableDictionary<string, S> ToImmutableDictionary<S>(
            (string key, S value)[] data
        ) {
            return ToImmutableDictionary<S, S>(data);
        }
        protected static ImmutableDictionary<string, S> ToImmutableDictionary<R, S>(
            (string key, R value)[] data
        ) where R : S {
            var dict = data.Aggregate(
                new Dictionary<string, S>(),
                (d, tuple) =>
                {
                    var (key, value) = tuple;
                    d.Add(key, value);
                    return d;
                }
            );
            return dict.ToImmutableDictionary();
        }
    }
}
