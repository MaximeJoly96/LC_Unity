using System.Collections.Generic;

namespace GameProgression
{
    public class PersistentDataHolder
    {
        private readonly Dictionary<string, object> _data;

        private static PersistentDataHolder _instance;

        public static PersistentDataHolder Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PersistentDataHolder();

                return _instance;
            }
        }

        private PersistentDataHolder()
        {
            _data = new Dictionary<string, object>();
        }

        public void StoreData(string key, object data)
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = data;
                return;
            }

            _data.Add(key, data);
        }

        public object GetData(string key)
        {
            return _data[key];
        }

        public void EraseData(string key)
        {
            _data.Remove(key);
        }
    }
}

