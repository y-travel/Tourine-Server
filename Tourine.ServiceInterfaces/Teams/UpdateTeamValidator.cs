using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class UpdateTeamValidator : AbstractValidator<UpdateTeam>
    {
        public UpdateTeamValidator()
        {
            RuleFor(t => t.Team.Id).NotEmpty();
            RuleFor(t => t.Team.TourId).NotEmpty();
        }
    }
}
