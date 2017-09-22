using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using ServiceStack;
using ServiceStack.Configuration;

namespace Tourine
{
    public class Settings
    {
        public IConfiguration Instance { get; }

        public string ConnectionString { get { return Get<string>(); } set { Set(value); } }

        public Settings(IConfiguration configuration)
        {
            Instance = configuration;
        }

        private T Get<T>([CallerMemberName] string key = "")
        {
            return Instance.GetValue<T>(key);
        }

        private void Set<T>(T value, [CallerMemberName] string key = "")
        {
            Instance[key] = value?.ToString();
        }
    }
}
