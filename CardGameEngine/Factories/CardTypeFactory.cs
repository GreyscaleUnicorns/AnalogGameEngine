using CardGameEngine.Entities;
using CardGameEngine.Management;

namespace CardGameEngine.Factories {
    public abstract class CardTypeFactory {
        internal void Initialize(Registry registry) {
            this.CreateCardTypes(registry);
        }

        protected abstract void CreateCardTypes(Registry registry);
    }
}
