using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class PostTourValidator : AbstractValidator<PostTour>
    {
        public PostTourValidator()
        {
            RuleFor(t => t.Tour.Code).NotEmpty();
            RuleFor(t => t.Tour.AgencyId).NotEmpty();
            RuleFor(t => t.Tour.Status).NotEmpty();
        }
    }
}
