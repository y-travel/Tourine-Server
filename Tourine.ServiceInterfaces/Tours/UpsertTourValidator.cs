using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class UpsertTourValidator : AbstractValidator<UpsertTour>
    {
        public UpsertTourValidator()
        {
            RuleFor(t => t.TourDetail.Duration).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(t => t.Capacity).NotEmpty().NotNull();
            RuleFor(t => t.BasePrice).NotEmpty().NotNull();
            RuleFor(t => t.TourDetail.DestinationId).NotEmpty().NotNull();
            RuleFor(t => t.TourDetail.StartDate).NotEmpty().NotNull();
            RuleFor(t => t.TourDetail.PlaceId).NotEmpty().NotNull();
            RuleFor(t => t.Options.Count).Equal(3);
        }
    }
}