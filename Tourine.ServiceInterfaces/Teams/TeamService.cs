using System;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Teams
{
    public class TeamService : AppService
    {
        [Authenticate]
        public object Post(CreateTeam team)
        {
            team.Team.Id = Guid.NewGuid();
            Db.Insert(team.Team);
            return Db.SingleById<Team>(team.Team.Id);
        }

        [Authenticate]
        public void Put(UpdateTeam team)
        {
            if (!Db.Exists<Team>(new { Id = team.Team.Id }))
                throw HttpError.NotFound("");
            Db.Update(team.Team);//@TODO: dont update submitDate
        }
    }
}
