using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    public class AddCustomerToAgencyValidator : AbstractValidator<AddCustomerToAgency>
    {
        public AddCustomerToAgencyValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
        }
    }
}
