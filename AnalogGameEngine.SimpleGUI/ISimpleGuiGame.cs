using System;

namespace AnalogGameEngine.SimpleGUI {
    public interface ISimpleGuiGame : IGameBase {
        void Run(Action doBefore);
    }
}
