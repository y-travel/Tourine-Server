using System.IO;
using Quartz;
using Tourine.ServiceInterfaces;

namespace Tourine
{
    public class Job : AppService, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string path = @"C:\logs\TourStatusChangeLog.txt";

            using (StreamWriter sw = new StreamWriter(path, true))
            {
//                var affectedRow = Db.ExecuteSql("EXEC UpdateTourSatus");
//                if (affectedRow != 0)
//                {
//                    sw.WriteLine(DateTime.Now + " - " + "EXEC UpdateTourSatus, number of rows affected : " + affectedRow);
//                    sw.WriteLine();
//                }

            }
        }
    }
}