using System;
using System.Linq;
using ServiceStack;
using Tourine.ServiceInterfaces.Models;
using System.Collections.Generic;
using DevExpress.Utils;

namespace Tourine.ServiceInterfaces.Common
{
    public static class Formatter
    {
        public static string GetDisplayTitle(this Person person) =>
            $"{(!person.IsInfant ? (person.Gender ? Strings.Mr : Strings.Mrs) : "")} {person.Name} {person.Family}";

        public static string GetDisplayTitle(this OptionType optionType, bool reverse = false) =>
            reverse
                ? $"{"WithOut".Loc()} "
                : $"";

        public static string GetItems(this Enum item) => Enum.GetNames(item.GetType()).Select(x => x.Loc()).Join("/");

        public static IEnumerable<T> MaskToList<T>(this Enum mask)
        {
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
                throw new ArgumentException();
            return Enum.GetValues(typeof(T))
                .Cast<Enum>()
                .Where(predicate: m =>
                {
                    return mask.HasAnyFlag(m);
                })
                .Cast<T>();
        }
    }
}
