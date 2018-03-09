using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Services
{
    public class PostServiceForPassengerValidator : AbstractValidator<PostServiceForPassenger>
    {
        public PostServiceForPassengerValidator()
        {
            RuleFor(s => s.PassengerList.PersonId).NotEmpty();
            RuleFor(s => s.PassengerList.TourId).NotEmpty();
            RuleFor(s => s.PassengerList.Type).NotEmpty();
            RuleFor(s => s.PassengerList.Status).NotEmpty();
        }
    }
}
