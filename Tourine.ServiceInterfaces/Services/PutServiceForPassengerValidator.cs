using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Services
{
    public class PutServiceForPassengerValidator : AbstractValidator<PutServiceForPassenger>
    {
        public PutServiceForPassengerValidator()
        {
            RuleFor(s => s.Service.Id).NotEmpty();
            RuleFor(s => s.Service.PassengerId).NotEmpty();
            RuleFor(s => s.Service.TourId).NotEmpty();
            RuleFor(s => s.Service.Type).NotEmpty();
            RuleFor(s => s.Service.Status).NotEmpty();
        }
    }
}
