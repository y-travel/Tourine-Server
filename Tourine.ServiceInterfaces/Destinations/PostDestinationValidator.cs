using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class PostDestinationValidator : AbstractValidator<PostDestination>
    {
        public PostDestinationValidator()
        {
            RuleFor(d => d.Destination.Name).NotEmpty();
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
