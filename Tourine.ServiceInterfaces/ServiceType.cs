using System;
using System.Collections.Generic;

namespace Tourine.ServiceInterfaces
{
    [Flags]
    public enum OptionType : long
    {
        Empty = 0,
        Room = 1,
        Bus = 2,
        Food = 4
    }
    public static class OptionTypeExtension
    {
        public static string GetEmojis(this OptionType OptionType)
        {
            string emojies = "";
            var types = new List<OptionType>();
            foreach (OptionType r in Enum.GetValues(typeof(OptionType)))
            {
                if ((OptionType & r) == r)
                {
                    types.Add(r);
                }
                else
                {
                    types.Add(OptionType.Empty);
                }
            }
            types.RemoveAt(0);
            foreach (OptionType t in types)
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

        public static string GetEmoji(this OptionType OptionType)
        {

            switch (OptionType)
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
        public static string GetDescription(this OptionType OptionType)
        {

            switch (OptionType)
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
