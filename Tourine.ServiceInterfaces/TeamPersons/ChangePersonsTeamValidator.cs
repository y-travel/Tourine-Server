using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class ChangePersonsTeamValidator : AbstractValidator<ChangePersonsTeam>
    {
        public ChangePersonsTeamValidator()
        {
            RuleFor(t => t.TeamPerson.Id).NotEmpty();
            RuleFor(t => t.TeamPerson.PersonId).NotEmpty();
            RuleFor(t => t.TeamPerson.TeamId).NotEmpty();
        }
    }
}
