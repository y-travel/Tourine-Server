using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Bot
{
    public partial class TourineBotCmdService
    {
        public class PersonServiceReport
        {
            public string Family { get; set; }
            public string Name { get; set; }
            public OptionType serviceSum { get; set; }
        }
    }
}
