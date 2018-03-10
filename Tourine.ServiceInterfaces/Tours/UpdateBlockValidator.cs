using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Tours
{
    public class UpdateBlockValidator : AbstractValidator<UpdateBlock>
    {
        public UpdateBlockValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.AgencyId).NotEmpty().NotNull();
            RuleFor(x => x.Capacity).NotEmpty().NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.ParentId).NotEmpty().NotNull();
        }
    }
}
