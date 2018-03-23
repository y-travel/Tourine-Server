using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

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
        public static List<PersonIncome> GetListOfTypes(this OptionType optionType)
        {
            var list = new List<PersonIncome>();

            if (optionType.Is(OptionType.Room))
                list.Add(new PersonIncome{OptionType = OptionType.Room });
            if (optionType.Is(OptionType.Bus))
                list.Add(new PersonIncome { OptionType = OptionType.Bus });
            if (optionType.Is(OptionType.Food))
                list.Add(new PersonIncome { OptionType = OptionType.Food });

            return list;
        }

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
                default:
                    return OptionStatus.Limited;
            }
        }
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
