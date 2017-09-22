using System;
using ServiceStack.Text;

namespace Tourine
{
    public static class JsConfigurator
    {
        public static void Init()
        {
            JsConfig.TreatEnumAsInteger = true;
            JsConfig.DateHandler = DateHandler.ISO8601;
            JsConfig.TimeSpanHandler = TimeSpanHandler.StandardFormat;
            JsConfig<Version>.DeSerializeFn = Version.Parse;
            JsConfig<Version>.SerializeFn = version => version?.ToString();
        }
    }
}
