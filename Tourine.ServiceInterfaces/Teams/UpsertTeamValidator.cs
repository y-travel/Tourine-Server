using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class UpsertTeamValidator : AbstractValidator<UpsertTeam>
    {
        public UpsertTeamValidator()
        {
            RuleFor(t => t.TourId).NotEmpty().NotNull();
            RuleFor(t => t.Buyer.PersonId).NotNull().NotEmpty();
            RuleFor(t => t.Buyer.PersonIncomes.Count).Equal(3);
        }
    }
}
