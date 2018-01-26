using System;
using System.IO;
using Quartz;

namespace Tourine
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string path = @"E:\Log.txt";

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("Message from HelloJob " + DateTime.Now);
            }
        }
    }
}