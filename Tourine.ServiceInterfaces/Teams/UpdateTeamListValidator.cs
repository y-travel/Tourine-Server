using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Teams
{
    class UpdateTeamListValidator : AbstractValidator<UpdateTeamList>
    {
        public UpdateTeamListValidator()
        {
            RuleFor(x => x.Teams.Count).GreaterThan(0);
            RuleFor(x => x.Teams).SetCollectionValidator(new TeamListValidator());
        }
    }

    public class TeamListValidator : AbstractValidator<Team>
    {
        public TeamListValidator()
        {
            RuleFor(x => x.InfantPrice).NotNull().NotEmpty();
            RuleFor(x => x.BasePrice).NotNull().NotEmpty();
        }
    }
}
