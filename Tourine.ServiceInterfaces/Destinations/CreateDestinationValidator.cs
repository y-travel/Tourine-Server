using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class CreateDestinationValidator : AbstractValidator<CreateDestination>
    {
        public CreateDestinationValidator()
        {
//            RuleFor(d => d.Destination.Name).NotNull();
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
