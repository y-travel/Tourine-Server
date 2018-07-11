using System;
using System.Collections.Generic;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Common
{
    [Flags]
    public enum OptionType
    {
        Empty = 0,
        Room = 1,
        Bus = 2,
        Food = 4,
        All = Empty | Room | Bus | Food
    }
    public static class OptionTypeExtension
    {
        public static OptionStatus GetDefaultStatus(this OptionType optionType)
        {
            switch (optionType)
            {
                case OptionType.Room:
                    return OptionStatus.Limited;
                case OptionType.Bus:
                    return OptionStatus.Limited;
                case OptionType.Food:
                    return OptionStatus.Unlimited;
                case OptionType.Empty:
                    return OptionStatus.Limited;
                default:
                    return OptionStatus.Limited;
            }
        }
        public static string GetEmojis(this OptionType optionType)
        {
            string emojies = "";
            var types = new List<OptionType>();
            foreach (OptionType r in Enum.GetValues(typeof(OptionType)))
                types.Add((optionType & r) == r ? r : OptionType.Empty);

            types.RemoveAt(0);
            foreach (var t in types)
            {
                switch (t)
                {
                    case OptionType.Room:
                        emojies += "🛏"; break;
                    case OptionType.Bus:
                        emojies += "🚌"; break;
                    case OptionType.Food:
                        emojies += "🍜"; break;

                    default:
                        emojies += "🚫"; break;
                }
            }
            return emojies;
        }

        public static string GetEmoji(this OptionType optionType)
        {

            switch (optionType)
            {
                case OptionType.Room:
                    return "🛏";
                case OptionType.Bus:
                    return "🚌";
                case OptionType.Food:
                    return "🍜";

                default:
                    return "🚫";
            }
        }
        public static string GetDescription(this OptionType optionType)
        {
            switch (optionType)
            {
                case OptionType.Room:
                    return "( تخت )";
                case OptionType.Bus:
                    return "(صندلی)";
                case OptionType.Food:
                    return "( غذا )";
                default:
                    return "(اینفنت)";
            }
        }
    }
}
