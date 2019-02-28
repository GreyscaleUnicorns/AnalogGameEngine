using AnalogGameEngine;
using AnalogGameEngine.Entities;

namespace AnalogGameEngine.SimpleGUI {
    public abstract class SimpleGuiConductor<T, U> : Conductor<T, U> where T : IGameBase where U : CardType {
        // Add stage for texture creation
    }
}
