using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;
using AnalogGameEngine.Management;

using MauMauPrototype.Effects;

namespace MauMauPrototype.Factories {
    public class MauMauEffectFactory : EffectFactory {
        protected override void CreateEffects(Game game, Registry registry) {
            new DrawTwoEffect("drawTwo", game, registry);
            new SkipEffect("skip", game, registry);
        }
    }
}
