using System;
using System.Collections.Generic;

namespace Tourine.ServiceInterfaces
{
    [Flags]
    public enum ServiceType : long
    {
        Empty = 0,
        Bed = 1,
        Bus = 2,
        Food = 4
    }
    public static class ServiceTypeExtension
    {
        public static string GetEmojis(this ServiceType ServiceType)
        {
            string emojies = "";
            var types = new List<ServiceType>();
            foreach (ServiceType r in Enum.GetValues(typeof(ServiceType)))
            {
                if ((ServiceType & r) == r)
                {
                    types.Add(r);
                }
                else
                {
                    types.Add(ServiceType.Empty);
                }
            }
            types.RemoveAt(0);
            foreach (ServiceType t in types)
            {
                switch (t)
                {
                    case ServiceType.Bed:
                        emojies += "🛏"; break;
                    case ServiceType.Bus:
                        emojies += "🚌"; break;
                    case ServiceType.Food:
                        emojies += "🍜"; break;

                    default:
                        emojies += "🚫"; break;
                }
            }
            return emojies;
        }

        public static string GetEmoji(this ServiceType ServiceType)
        {

            switch (ServiceType)
            {
                case ServiceType.Bed:
                    return "🛏";
                case ServiceType.Bus:
                    return "🚌";
                case ServiceType.Food:
                    return "🍜";

                default:
                    return "🚫";
            }
        }
        public static string GetDescription(this ServiceType ServiceType)
        {

            switch (ServiceType)
            {
                case ServiceType.Bed:
                    return "( تخت )";
                case ServiceType.Bus:
                    return "(صندلی)";
                case ServiceType.Food:
                    return "( غذا )";
                default:
                    return "(اینفنت)";
            }
        }
    }
}
