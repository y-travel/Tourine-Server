using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class ReserveBlockValidator: AbstractValidator<ReserveBlock> {
        public ReserveBlockValidator()
        {
            RuleFor(x => x.ParentId).NotEmpty().NotNull();
            RuleFor(x => x.AgencyId).NotEmpty().NotNull();
            RuleFor(x => x.Capacity).GreaterThanOrEqualTo(1);
        }
    }
}
