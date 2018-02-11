using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TourDetails
{
    public class UpdateTourDetailValidator : AbstractValidator<UpdateTourDetail>
    {
        public UpdateTourDetailValidator()
        {
            RuleFor(t => t.TourDetail.Id).NotEmpty();
            RuleFor(t => t.TourDetail.DestinationId).NotEmpty();
            RuleFor(t => t.TourDetail.CreationDate).NotEmpty();
        }
    }
}
