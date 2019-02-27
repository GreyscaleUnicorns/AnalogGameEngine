using AnalogGameEngine.Entities;
using AnalogGameEngine.Factories;
using AnalogGameEngine.Management;

using MauMauConsolePrototype.Effects;

namespace MauMauConsolePrototype.Factories {
    public class MauMauEffectFactory : EffectFactory {
        protected override void CreateEffects(Game game, Registry registry) {
            new DrawTwoEffect("drawTwo", game, registry);
            new SkipEffect("skip", game, registry);
        }
    }
}
