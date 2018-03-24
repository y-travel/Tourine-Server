using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.ServiceInterfaces
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NotPopulateAttribute : AttributeBase
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PopulateAttribute : AttributeBase
    {
    }

    public static class PopulateExtension
    {
        public static To SafePopulate<To, From>(this To to, From from)
        {
            var hasIncludeAttrib = to.ContainPopulateAttribute();
            var hasExcludeAttrib = to.ContainNotPopulateAttribute();
            if (hasExcludeAttrib && hasIncludeAttrib)
                throw new Exception(to.ToString() + " should not be contain both Populate and NotPopulate attributes");

            return hasIncludeAttrib
                ? to.PopulateFromPropertiesWithAttribute(@from, typeof(PopulateAttribute))
                : (
                    hasExcludeAttrib
                        ? to.PopulateFromPropertiesWithoutAttribute(@from, typeof(NotPopulateAttribute))
                        : to.PopulateWith(@from)
                );
        }

        public static bool ContainPopulateAttribute<T>(this T source) => source.ContainAttribute(typeof(PopulateAttribute));
        public static bool ContainNotPopulateAttribute<T>(this T source) => source.ContainAttribute(typeof(NotPopulateAttribute));
    }
}
