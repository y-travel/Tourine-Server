using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class PostTourValidator : AbstractValidator<PostTour>
    {
        public PostTourValidator()
        {
            RuleFor(t => t.Tour.DestinationId).NotEmpty();
            RuleFor(t => t.Tour.AdultCount).NotEmpty();
            RuleFor(t => t.Tour.Code).NotEmpty();
            RuleFor(t => t.Tour.Duration).NotEmpty();
            RuleFor(t => t.Tour.IsFlight).NotEmpty();
            RuleFor(t => t.Tour.PlaceId).NotEmpty();
            RuleFor(t => t.Tour.StartDate).NotEmpty();
            RuleFor(t => t.Tour.StartDate).GreaterThanOrEqualTo(t => t.Tour.CreationDate);
        }
    }
}
