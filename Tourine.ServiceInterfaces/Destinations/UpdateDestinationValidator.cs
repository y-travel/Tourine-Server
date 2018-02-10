using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class UpdateDestinationValidator : AbstractValidator<UpdateDestination>
    {
        public UpdateDestinationValidator()
        {
            RuleFor(d => d.Destination.Id).NotEmpty();
            RuleFor(d => d.Destination.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
