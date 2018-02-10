using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class PutTourValidator : AbstractValidator<PutTour>
    {
        public PutTourValidator()
        {
            RuleFor(t => t.Tour.Id).NotEmpty();
            RuleFor(t => t.Tour.Code).NotEmpty();
            RuleFor(t => t.Tour.TourDetailId).NotEmpty();
        }
    }
}