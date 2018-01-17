using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Users
{
    public class UserValidator : AbstractValidator<PostUser>
    {
        public UserValidator()
        {
            RuleFor(user => user.User.Username).NotEmpty();
            RuleFor(user => user.User.Password.Length).GreaterThanOrEqualTo(8);
        }
    }
}
