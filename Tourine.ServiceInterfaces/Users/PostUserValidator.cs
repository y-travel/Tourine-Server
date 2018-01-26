using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Users
{
    public class PostUserValidator : AbstractValidator<PostUser>
    {
        public PostUserValidator()
        {
            RuleFor(u => u.User.CustomerId).NotEmpty();
            RuleFor(u => u.User.Password.Length).GreaterThanOrEqualTo(8);
            RuleFor(u => u.User.Username).NotEmpty();
            RuleFor(u => u.User.Role).NotEmpty();
        }
    }
}
