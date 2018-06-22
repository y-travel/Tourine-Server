using System;
using System.Linq;
using ServiceStack;
using Tourine.ServiceInterfaces.Models;
using System.Collections.Generic;

namespace Tourine.ServiceInterfaces.Common
{
    public static class Formatter
    {
        public static string GetDisplayTitle(this Person person) =>
            $"{(!person.IsInfant ? (person.Gender ? Strings.Mr : Strings.Mrs) : "")} {person.Name} {person.Family}";
        public static string GetDisplayTitleEn(this Person person) =>
            $"{person.EnglishName} {person.EnglishFamily}";

        public static string GetDisplayTitle(this OptionType optionType, bool reverse = false)
        {
            if (optionType == OptionType.Empty)
                return Strings.NoneOption;
            var options = (reverse ? ~optionType : optionType).GetMaskArray<OptionType>();
            if (options.Length == 0 || options.Length == Enum.GetNames(typeof(OptionType)).Length - 1)
                return "";
            var formattedOption = options.Select(x => x.Loc()).Join("/");
            return reverse
                ? $"{Strings.Without} {formattedOption}"
                : $"{Strings.With} {formattedOption}";
        }

        public static string GetItems(this Enum item) => Enum.GetNames(item.GetType()).Select(x => x.Loc()).Join("/");

        public static string[] GetMaskArray<T>(this Enum mask)
        {
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
                throw new ArgumentException();
            return Enum.GetValues(typeof(T))
                .Cast<Enum>()
                .Where(predicate: x => (int)Enum.Parse(typeof(OptionType), x.ToString()) > 0 && mask.HasFlag(x))
                .Select(x => x.ToString()).ToArray();
        }
    }
}
