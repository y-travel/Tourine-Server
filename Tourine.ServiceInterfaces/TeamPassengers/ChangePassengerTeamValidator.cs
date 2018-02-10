using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class ChangePassengerTeamValidator : AbstractValidator<ChangePassengerTeam>
    {
        public ChangePassengerTeamValidator()
        {
            RuleFor(t => t.TeamPassenger.Id).NotEmpty();
            RuleFor(t => t.TeamPassenger.PassengerId).NotEmpty();
            RuleFor(t => t.TeamPassenger.TeamId).NotEmpty();
        }
    }
}
