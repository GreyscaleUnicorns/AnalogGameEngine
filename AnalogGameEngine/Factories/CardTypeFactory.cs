using AnalogGameEngine.Entities;
using AnalogGameEngine.Management;

namespace AnalogGameEngine.Factories {
    public abstract class CardTypeFactory {
        internal void Initialize(Registry registry) {
            this.CreateCardTypes(registry);
        }

        protected abstract void CreateCardTypes(Registry registry);
    }
}
