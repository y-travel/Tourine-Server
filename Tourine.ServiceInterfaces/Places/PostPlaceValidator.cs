using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Places
{
    public class PostPlaceValidator : AbstractValidator<PostPlace>
    {
        public PostPlaceValidator()
        {
            RuleFor(p => p.Place.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
