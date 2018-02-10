using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    public class UpdateCustomerToAgencyValidator : AbstractValidator<UpdateCustomerToAgency>
    {
        public UpdateCustomerToAgencyValidator()
        {
            RuleFor(a => a.AgencyId).NotEmpty();
            RuleFor(a => a.CustomerId).NotEmpty();
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
