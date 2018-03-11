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
            RuleFor(s => s.PassengerList.OptionType).NotEmpty();
            RuleFor(s => s.PassengerList.IncomeStatus).NotEmpty();
        }
    }
}
