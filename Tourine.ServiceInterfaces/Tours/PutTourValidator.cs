using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class PutTourValidator : AbstractValidator<PutTour>
    {
        public PutTourValidator()
        {
            RuleFor(t => t.Tour.Id).NotEmpty();
            RuleFor(t => t.Tour.DestinationId).NotEmpty();
            RuleFor(t => t.Tour.AdultCount).NotEmpty();
            RuleFor(t => t.Tour.Code).NotEmpty();
            RuleFor(t => t.Tour.Duration).NotEmpty();
            RuleFor(t => t.Tour.IsFlight).NotEmpty();
            RuleFor(t => t.Tour.PlaceId).NotEmpty();
            RuleFor(t => t.Tour.StartDate).NotEmpty();
        }
    }
}