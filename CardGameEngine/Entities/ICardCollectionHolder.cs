using System;
using System.Collections.Immutable;

namespace CardGameEngine.Entities {
    public interface ICardCollectionHolder {
        ImmutableDictionary<string, Set> Sets { get; }
        ImmutableDictionary<string, Stack> Stacks { get; }
    }
}
