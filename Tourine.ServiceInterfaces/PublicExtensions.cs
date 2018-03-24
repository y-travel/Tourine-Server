using System;
using System.Linq;
using System.Reflection;

namespace Tourine.ServiceInterfaces
{
    public static class PublicExtensions
    {
        public static bool ContainAttribute<T>(this T source, Type attributeType) =>
            typeof(T).GetProperties().FirstOrDefault(x => Attribute.IsDefined(x, attributeType)) != null;
    }
}