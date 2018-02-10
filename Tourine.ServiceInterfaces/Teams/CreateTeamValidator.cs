using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    public class CreateTeamValidator : AbstractValidator<CreateTeam>
    {
        public CreateTeamValidator()
        {
            RuleFor(t => t.Team.Id).Empty();
            RuleFor(t => t.Team.TourId).NotEmpty();
            RuleFor(t => t.Team.SubmitDate).NotEmpty();
        }
    }
}
