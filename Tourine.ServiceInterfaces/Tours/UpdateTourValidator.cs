using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class UpdateTourValidator : AbstractValidator<UpdateTour>
    {
        public UpdateTourValidator()
        {
            RuleFor(t => t.Tour.Id).NotEmpty();
            RuleFor(t => t.Tour.Code).NotEmpty();
            RuleFor(t => t.Tour.TourDetailId).NotEmpty();
        }
    }
}