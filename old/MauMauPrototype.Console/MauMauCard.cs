using AnalogGameEngine.Entities;

namespace MauMauPrototype {
    public class MauMauCard : Card<MauMauCardType> {
        public MauMauCard(MauMauCardType type, CardCollectionBase collection) : base(type, collection) { }
    }
}
