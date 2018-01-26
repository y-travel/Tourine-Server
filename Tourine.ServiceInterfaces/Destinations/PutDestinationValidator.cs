using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class PutDestinationValidator : AbstractValidator<PutDestination>
    {
        public PutDestinationValidator()
        {
            RuleFor(d => d.Destination.Id).NotEmpty();
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
