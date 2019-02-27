using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

using MauMauConsolePrototype.Effects;

namespace MauMauConsolePrototype.CardTypes {
    class Eight : MauMauCardType {
        public Eight(Registry registry, Colors color) : base(registry, color, Values.Eight) {
            this.Effects.Add(registry.GetEffect("skip"));
        }
    }
}
