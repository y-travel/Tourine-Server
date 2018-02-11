using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class CreateTourValidator : AbstractValidator<CreateTour>
    {
        public CreateTourValidator()
        {
            RuleFor(t => t.Capacity).NotEmpty();
            RuleFor(t => t.BasePrice).NotEmpty();
            RuleFor(t => t.TourDetail.DestinationId).NotEmpty();
            RuleFor(t => t.TourDetail.StartDate).NotEmpty();
            RuleFor(t => t.TourDetail.PlaceId).NotEmpty();
        }
    }
}
