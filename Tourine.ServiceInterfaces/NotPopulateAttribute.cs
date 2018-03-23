using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.ServiceInterfaces
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class NotPopulateAttribute : AttributeBase
    {
    }
}
