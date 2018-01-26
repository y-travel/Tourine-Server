using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Places
{
    public class PutPlaceValidator : AbstractValidator<PutPlace>
    {
        public PutPlaceValidator()
        {
            RuleFor(p => p.Place.Id).NotEmpty();
            RuleFor(p => p.Place.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}