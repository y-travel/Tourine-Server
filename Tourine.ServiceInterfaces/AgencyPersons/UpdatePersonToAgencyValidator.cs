using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    public class UpdatePersonToAgencyValidator : AbstractValidator<UpdateCustomerToAgency>
    {
        public UpdatePersonToAgencyValidator()
        {
            RuleFor(a => a.AgencyId).NotEmpty();
            RuleFor(a => a.PersonId).NotEmpty();
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
