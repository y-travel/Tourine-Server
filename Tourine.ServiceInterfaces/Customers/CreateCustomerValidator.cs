using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Customers
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(p => p.Customer.Family).NotEmpty();
            RuleFor(p => p.Customer.MobileNumber).NotEmpty();
            RuleFor(p => p.Customer.Name).NotEmpty();
        }
    }
}