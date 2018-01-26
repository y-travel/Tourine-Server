using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Customers
{
    public class PostCustomerValidator : AbstractValidator<PostCustomer>
    {
        public PostCustomerValidator()
        {
            RuleFor(p => p.Customer.Family).NotEmpty();
            RuleFor(p => p.Customer.MobileNumber).NotEmpty();
            RuleFor(p => p.Customer.Name).NotEmpty();
            RuleFor(p => p.Customer.Phone).NotEmpty();
        }
    }
}