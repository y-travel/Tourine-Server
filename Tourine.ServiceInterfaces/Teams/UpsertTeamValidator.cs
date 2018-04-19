using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class UpsertTeamValidator : AbstractValidator<UpsertTeam>
    {
        public UpsertTeamValidator()
        {
            RuleFor(t => t.TourId).NotEmpty().NotNull();
            RuleFor(t => t.Buyer.Person.Id).NotNull().NotEmpty();
            RuleFor(t => t.Buyer.PersonIncomes.Count).Equal(3);
            RuleFor(t => t.Buyer.Person.IsInfant).NotEqual(true);
            RuleFor(t => t.Buyer.Person.IsUnder5).NotEqual(true);
        }
    }
}
