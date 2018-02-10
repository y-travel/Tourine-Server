using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class AddPassengerToTeamValidator : AbstractValidator<AddPassengerToTeam>
    {
        public AddPassengerToTeamValidator()
        {
            RuleFor(t => t.TeamPassenger.PassengerId).NotEmpty();
            RuleFor(t => t.TeamPassenger.TeamId).NotEmpty();
        }
    }
}
