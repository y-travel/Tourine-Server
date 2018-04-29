using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class UpsertTeamValidator : AbstractValidator<UpsertTeam>
    {
        public UpsertTeamValidator()
        {
            RuleFor(t => t.TourId).NotEmpty().NotNull();
            RuleFor(t => t.Buyer.Id).NotNull().NotEmpty();
            RuleFor(t => t.Buyer.IsInfant).NotEqual(true);
            RuleFor(t => t.Buyer.IsUnder5).NotEqual(true);
        }
    }
}
