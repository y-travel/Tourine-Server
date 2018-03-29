using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class PostServiceForPassengerValidator : AbstractValidator<PostServiceForPassenger>
    {
        public PostServiceForPassengerValidator()
        {
            RuleFor(s => s.PassengerList.PersonId).NotEmpty();
            RuleFor(s => s.PassengerList.TourId).NotEmpty();
            RuleFor(s => s.PassengerList.OptionType).NotEmpty();
            RuleFor(s => s.PassengerList.IncomeStatus).NotEmpty();
        }
    }
}
