using ServiceStack;

namespace Tourine.ServiceInterfaces
{
    [Route("/disBot", "GET")]
    public class TourineBotDisabler : IReturn<string>
    {
    }

    [Route("/enBot", "GET")]
    public class TourineBotEnabler : IReturn<string>
    {
    }

    public class TourineBotService : AppService
    {
        public TourineBot Bot { get; set; }
        public object Get(TourineBotDisabler disabler)
        {
            Bot.Stop();
            return "Stopped";
        }

        public object Get(TourineBotEnabler enabler)
        {
            Bot?.Start();
            return "Started";
        }
    }
}
