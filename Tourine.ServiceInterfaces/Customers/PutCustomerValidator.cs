using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Customers
{
    public class PutCustomerValidator : AbstractValidator<PutCustomer>
    {
        public PutCustomerValidator()
        {
            RuleFor(c => c.Customer.Id).NotEmpty();
            RuleFor(c => c.Customer.Family).NotEmpty();
            RuleFor(c => c.Customer.MobileNumber).NotEmpty();
            RuleFor(c => c.Customer.Name).NotEmpty();
            RuleFor(c => c.Customer.Phone).NotEmpty();
        }
    }
}
