using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class PassengerListServiceTest:ServiceTest<PassengerListService>
    {
        [Test]
        public void duplication_in_passenger_list_upsert_should_throw_exception()
        {
            
        }
    }
}
