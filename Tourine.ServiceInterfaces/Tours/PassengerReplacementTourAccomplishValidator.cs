using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class PassengerReplacementTourAccomplishValidator : AbstractValidator<PassengerReplacementTourAccomplish>
    {
        public PassengerReplacementTourAccomplishValidator()
        {
            RuleFor(x => x.OldTourId).NotNull().NotEmpty();
            RuleFor(x => x.TourId).NotNull().NotEmpty();
            RuleFor(x => x.BasePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.InfantPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.FoodPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.RoomPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.BusPrice).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
