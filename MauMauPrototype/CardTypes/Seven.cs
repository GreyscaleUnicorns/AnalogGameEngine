using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

using MauMauPrototype.Effects;

namespace MauMauPrototype.CardTypes {
    class Seven : MauMauCardType {
        public Seven(Registry registry, Colors color) : base(registry, color, Values.Seven) {
            this.Effects.Add(registry.GetEffect("drawTwo"));
        }
    }
}
