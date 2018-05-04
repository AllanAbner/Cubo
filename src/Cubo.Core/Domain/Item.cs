using System;

namespace Cubo.Core.Domain {
    public class Item : Entity {

        public string Key { get; private set; }
        public string Value { get; set; }

        protected Item () {

        }

        public Item (string key, string value) {
            Key = key.ToLowerInvariant ();
            Value = value;
        }
    }
}