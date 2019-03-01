using AnalogGameEngine.Entities;
using System;
using System.Linq;

using MauMauPrototype.Effects;

namespace MauMauPrototype {
    class Program {
        static void Main(string[] args) {
            var conductor = new MauMauConductor();
            var game = conductor.StartGame();
        }
    }
}
