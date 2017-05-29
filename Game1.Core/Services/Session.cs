using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core.Services
{
    public class Session
    {
        private static Dictionary<string, Entry> _entries = new Dictionary<string, Entry>();

        public void Set<T>(string key, T value)
        {
            if (_entries.ContainsKey(key))
            {
                _entries.Remove(key);
            }

            _entries.Add(key, new Entry
            {
                Type = typeof(T),
                Value = value
            });
        }

        public T Get<T>(string key)
        {
            var entry = _entries[key];
            if (entry.Type == typeof(T))
            {
                return (T) entry.Value;
            }

            throw new InvalidCastException(typeof(T).ToString());
        }

        public bool Contains<T>(string key)
        {
            return _entries.ContainsKey(key) && _entries[key].Type == typeof(T);
        }

        private class Entry
        {
            public Type Type { get; set; }
            public object Value { get; set; }
        }
    }
}
