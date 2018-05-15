using System;
using System.Linq;
using System.Reflection;
using ServiceStack;
using ServiceStack.Logging;

namespace Tourine.ServiceInterfaces
{
    public static class PublicExtensions
    {
        public static bool ContainAttribute<T>(this T source, Type attributeType) =>
            typeof(T).GetProperties().FirstOrDefault(x => Attribute.IsDefined(x, attributeType)) != null;

        public static string GetRootDirectory()
        {
            return HostContext.AppHost?.VirtualFileSources?.RootDirectory.RealPath ?? "";
        }
    }
}