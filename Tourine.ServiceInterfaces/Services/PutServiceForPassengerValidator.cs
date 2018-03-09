using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Services
{
    public class PutServiceForPassengerValidator : AbstractValidator<PutServiceForPassenger>
    {
        public PutServiceForPassengerValidator()
        {
            RuleFor(s => s.PassengerList.Id).NotEmpty();
            RuleFor(s => s.PassengerList.PersonId).NotEmpty();
            RuleFor(s => s.PassengerList.TourId).NotEmpty();
            RuleFor(s => s.PassengerList.Type).NotEmpty();
            RuleFor(s => s.PassengerList.Status).NotEmpty();
        }
    }
}
