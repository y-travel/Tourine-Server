using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Services
{
    public class PostServiceForPassengerValidator : AbstractValidator<PostServiceForPassenger>
    {
        public PostServiceForPassengerValidator()
        {
            RuleFor(s => s.Service.PersonId).NotEmpty();
            RuleFor(s => s.Service.TourId).NotEmpty();
            RuleFor(s => s.Service.Type).NotEmpty();
            RuleFor(s => s.Service.Status).NotEmpty();
        }
    }
}
