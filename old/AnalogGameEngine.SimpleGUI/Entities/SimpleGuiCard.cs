using AnalogGameEngine.Entities;

namespace AnalogGameEngine.SimpleGUI.Entities {
    public class SimpleGuiCard<T> : Card<T> where T : SimpleGuiCardType {
        public SimpleGuiCard(T type, CardCollectionBase collection) : base(type, collection) { }
    }
}
