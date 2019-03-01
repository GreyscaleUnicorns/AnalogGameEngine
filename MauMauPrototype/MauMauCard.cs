using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Entities;

namespace MauMauPrototype {
    public class MauMauCard : SimpleGuiCard<MauMauCardType> {
        public MauMauCard(MauMauCardType type, CardCollectionBase collection) : base(type, collection) { }
    }
}
