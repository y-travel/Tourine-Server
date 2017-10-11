using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ServiceStack.Configuration;

namespace Tourine.Common
{
    public class Settings : IAppSettings
    {

        public string ConnectionString { get { return Get<string>(); } set { Set(value); } }

        private IAppSettings Instance { get; }

        public Settings(IAppSettings instance)
        {
            Instance = instance;
        }

        public Settings(Dictionary<string, string> dictionary = null)
        {
            Instance = new DictionarySettings(dictionary);
        }

        public Dictionary<string, string> GetAll() => Instance.GetAll();

        public List<string> GetAllKeys() => Instance.GetAllKeys();

        public bool Exists(string key) => Instance.Exists(key);

        public void Set<T>(string key, T value) => Instance.Set(key, value);

        public string GetString(string name) => Instance.GetString(name);

        public IList<string> GetList(string key) => Instance.GetList(key);

        public IDictionary<string, string> GetDictionary(string key) => Instance.GetDictionary(key);

        T IAppSettings.Get<T>(string name) => Instance.Get<T>(name);

        public T Get<T>(string name, T defaultValue) => Instance.Get(name, defaultValue);

        private T Get<T>([CallerMemberName] string key = null) => Instance.Get<T>(key);

        private void Set<T>(T value, [CallerMemberName] string key = null) => Instance.Set(key, value);
    }
}