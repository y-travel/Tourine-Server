using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    public class AddPersonToAgencyValidator : AbstractValidator<AddPersonToAgency>
    {
        public AddPersonToAgencyValidator()
        {
            RuleFor(c => c.PersonId).NotEmpty();
        }
    }
}
