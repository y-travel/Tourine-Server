using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TourDetails
{
    public class GetTourDetailValidator : AbstractValidator<GetTourDetail>
    {
        public GetTourDetailValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
        }
    }
}
