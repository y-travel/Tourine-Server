using System;
using System.Linq;
using System.Reflection;
using Funq;
using ServiceStack;

namespace Tourine.Common
{
    public static class ContainerExtensions
    {
        private static readonly MethodInfo ResolveMethod = typeof(Container).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .First(x => x.Name == nameof(Container.TryResolve) &&
                        x.GetGenericArguments().Length == 1 &&
                        x.GetParameters().Length == 0);

        public static object MyTryResolve(this Container container, Type type)
        {
            return ResolveMethod.MakeGenericMethod(type).Invoke(container, new object[0]);
        }

        public static void RegisterGeneric(this Container container, Type type, params Assembly[] assemblies)
        {
            foreach (var t in
                assemblies.SelectMany(x => x.GetTypes())
                    .Where(x => x.IsOrHasGenericInterfaceTypeOf(type) && x.BaseType != null))
            {
                if (t.BaseType == null) continue;
                var genericType = type.MakeGenericType(t.BaseType.GenericTypeArguments);
                container.RegisterAutoWiredType(t, genericType);
            }
        }

    }
}