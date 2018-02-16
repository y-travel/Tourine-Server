using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Users
{
    public class PutUserValidator : AbstractValidator<PutUser>
    {
        public PutUserValidator()
        {
            RuleFor(u => u.User.PersonId).NotEmpty();
            RuleFor(u => u.User.Password.Length).GreaterThanOrEqualTo(8);
            RuleFor(u => u.User.Username).NotEmpty();
            RuleFor(u => u.User.Role).NotEmpty();
        }
    }
}