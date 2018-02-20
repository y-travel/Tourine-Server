using System;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    public class AddPersonToAgencyValidator : AbstractValidator<AddPersonToAgency>
    {
        public AddPersonToAgencyValidator()
        {
            RuleFor(c => c.PersonId).NotEqual(Guid.Empty);
            RuleFor(c => c.AgencyId).NotEqual(Guid.Empty).NotNull();
        }
    }
}
