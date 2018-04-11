using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class UpsertTourValidator : AbstractValidator<UpsertTour>
    {
        public UpsertTourValidator()
        {
            RuleFor(t => t.Capacity).NotEmpty().NotNull();
            RuleFor(t => t.BasePrice).NotEmpty().NotNull();
            When(t => !t.IsBlock, () =>
                {
                    RuleFor(t => t.TourDetail.Duration).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
                    RuleFor(t => t.TourDetail.DestinationId).NotEmpty().NotNull();
                    RuleFor(t => t.TourDetail.StartDate).NotEmpty().NotNull();
                    RuleFor(t => t.TourDetail.PlaceId).NotEmpty().NotNull();
                });
            RuleFor(t => t.ParentId).NotEqual(x => x.Id);
            RuleFor(t => t.Options.Count).Equal(3);
        }
    }
}