using System.Collections.Generic;
using System.Collections.Immutable;

namespace CardGameEngine.Entities {
    public class SelfRegistry<T> where T : SelfRegistry<T> {
        private static Dictionary<string, T> Instances { get; set; }

        public SelfRegistry(string name) {
            if (Instances == null) {
                Instances = new Dictionary<string, T>();
            }

            Instances.Add(name, (T)this);
        }

        public static T Get(string name) => Instances[name];
    }
}
