using System.Collections.Generic;
using System.Collections.Immutable;

namespace CardGameEngine.Entities {
    public abstract class SelfRegistry<T> where T : SelfRegistry<T> {
        private static Dictionary<string, T> instances;
        private static Dictionary<string, T> Instances {
            get {
                if (instances == null) {
                    instances = new Dictionary<string, T>();
                }
                return instances;
            }
        }

        protected SelfRegistry() {
            // Nothing to do
        }

        public static T Get(string name) => Instances[name];

        public static T Register<U>(string name) where U : T, new() {
            if (!Instances.ContainsKey(name)) {
                Instances.Add(name, new U());
            }

            return Instances[name];
        }
    }
}
