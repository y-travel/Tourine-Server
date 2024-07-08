using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Models
{
    public class UpdatePersonToAgencyValidator : AbstractValidator<UpdatePersonToAgency>
    {
        public UpdatePersonToAgencyValidator()
        {
            RuleFor(a => a.AgencyId).NotEmpty();
            RuleFor(a => a.PersonId).NotEmpty();
            RuleFor(a => a.Id).NotEmpty();
        }
    }

    [Route("/agencies/persons", "PUT")]
    public class UpdatePersonToAgency : IReturn
    {
        public Guid Id { get; set; }
        public Guid AgencyId { get; set; }
        public Guid PersonId { get; set; }
    }

    [Route("/agencies/persons/", "GET")]
    public class GetPersonOfAgency : QueryDb<Person> , IJoin<Person,AgencyPerson>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
    }

    public class AddPersonToAgencyValidator : AbstractValidator<AddPersonToAgency>
    {
        public AddPersonToAgencyValidator()
        {
            RuleFor(c => c.PersonId).NotEqual(Guid.Empty);
            RuleFor(c => c.AgencyId).NotEqual(Guid.Empty).NotNull();
        }
    }

    [Route("/agencies/persons", "POST")]
    public class AddPersonToAgency : IReturn<AgencyPerson>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
        public Guid PersonId { get; set; }
    }

    public class AgencyPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }
    }
}
