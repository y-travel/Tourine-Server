using System;
using ServiceStack;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    [Route("/notify/agency/tour/{TourId}/role/{Role}", "POST")]
    public class SendNotifyToTourAgencies : IReturn
    {
        public Guid TourId { get; set; }
        public Role Role { get; set; }
        public string Msg { get; set; }
    }

    public class Agency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateAgencyValidator : AbstractValidator<UpdateAgency>
    {
        public UpdateAgencyValidator()
        {
            RuleFor(a => a.Agency.Id).NotEmpty();
            RuleFor(a => a.Agency.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(a => a.Agency.PhoneNumber).NotEmpty();
        }
    }

    [Route("/agencies", "PUT")]
    public class UpdateAgency : IReturn
    {
        public Agency Agency { get; set; }
    }

    [Route("/agencies/{id}", "GET")]
    public class GetAgency : IReturn<Agency>
    {
        public Guid Id { get; set; }
    }

    [Route("/agencies/{IsAll}", "GET")]
    public class GetAgencies : QueryDb<Agency>
    {
        public bool IsAll { get; set; }
    }

    [Route("/agencies/find/str", "GET")]
    public class FindAgency : QueryDb<Agency>
    {
        public string str { get; set; }
    }

    public class CreateAgencyValidator : AbstractValidator<CreateAgency>
    {
        public CreateAgencyValidator()
        {
            RuleFor(agency => agency.Agency.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(agency => agency.Person.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(agency => agency.Person.Family).NotEmpty();
        }
    }

    [Route("/agencies", "POST")]
    public class CreateAgency : IReturn<Agency>
    {
        public Agency Agency { get; set; }
        public Person Person { get; set; }
    }

}
