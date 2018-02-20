﻿using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    [Route("/agencies/persons", "POST")]
    public class AddPersonToAgency : IReturn<AgencyPerson>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
        public Guid PersonId { get; set; }
    }
}
