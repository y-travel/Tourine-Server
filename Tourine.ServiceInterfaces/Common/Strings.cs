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
        public const string Without = "بدون";
        public const string NoneOption = "بدون خدمات";
        public const string With = "با";
        public const string Adult = "بزرگسال";
        public const string PassengerReportFileName = "لیست مسافرین تور";
        public const string TicketReportFileName = "لیست بلیط";
        public const string VisaReportFileName = "لیست ویزا";
        public const string Agency = "آژانس";
    }

    public static class StringsExtensions
    {
        public static string Loc(this string str) => (Store.Dictionary.ContainsKey(str) ? Store.Dictionary[str] : str).ToString();

        public static class Store
        {
            public static Hashtable Dictionary = new Hashtable();
            static Store()
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
                Dictionary[nameof(OptionType.Room)] = Strings.Bed;
                Dictionary[nameof(OptionType.Food)] = Strings.Food;
                Dictionary[nameof(OptionType.Bus)] = Strings.Seat;

            }

        }
    }
}
