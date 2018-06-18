using System.Collections;
using System.Reflection;

namespace Tourine.ServiceInterfaces.Common
{
    public static class Strings
    {
        public const string Mr = "آقای";
        public const string Mrs = "خانم";
        public const string Bed = "تخت";
        public const string Food = "غذا";
        public const string Seat = "صندلی";
        public const string WithOut = "بدون";
        public static string Loc(this string str) => (Dynamic.Dictionary.ContainsKey(str) ? Dynamic.Dictionary[str] : str).ToString();
        public static class Dynamic
        {
            public static Hashtable Dictionary = new Hashtable();
            static Dynamic()
            {
                FillWithStringsConstants();
                CustomTranslation();
            }

            internal static void FillWithStringsConstants()
            {
                var fieldInfos = typeof(Strings).GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var fi in fieldInfos)
                    if (fi.IsLiteral && !fi.IsInitOnly)
                        Dictionary[fi.Name] = fi.GetRawConstantValue();

            }
            internal static void CustomTranslation()
            {
                Dictionary[nameof(OptionType.Room)] = Bed;
                Dictionary[nameof(OptionType.Food)] = Food;
                Dictionary[nameof(OptionType.Bus)] = Seat;

            }

        }
    }
}
