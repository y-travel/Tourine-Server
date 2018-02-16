using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class AddPersonsToTeamValidator : AbstractValidator<AddPersonToTeam>
    {
        public AddPersonsToTeamValidator()
        {
            RuleFor(t => t.TeamPerson.PersonId).NotEmpty();
            RuleFor(t => t.TeamPerson.TeamId).NotEmpty();
        }
    }
}
