using System;
using System.Collections.Generic;

namespace NoNameGame.Core.Services
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

        public bool TryGet<T>(string key, out T value)
        {
            value = default;
            if (_entries.TryGetValue(key, out Entry entry))
            {
                value = (T)entry.Value;
                return true;
            }
            return false;
        }

        private class Entry
        {
            public Type Type { get; set; }
            public object Value { get; set; }
        }
    }
}
