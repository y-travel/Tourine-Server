using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class CreateTeamValidator : AbstractValidator<CreateTeam>
    {
        public CreateTeamValidator()
        {
            RuleFor(t => t.TourId).NotEmpty().NotNull();
            RuleFor(t => t.Buyer.PersonId).NotNull().NotEmpty();
            RuleFor(t => t.Buyer.PersonIncomes.Count).Equal(3);
        }
    }
}
