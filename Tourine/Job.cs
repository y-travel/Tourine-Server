using System.IO;
using Quartz;
using Tourine.ServiceInterfaces;

namespace Tourine
{
    public class Job : AppService, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine("unusedjob-counter+");
        }
    }
}