using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public int Count { get; set; }
        public DateTime SubmitDate { get; set; } = DateTime.Now;

        [References(typeof(Person))]
        public Guid BuyerId { get; set; }
        [Reference]
        public Person Buyer { get; set; }

        [Ignore]
        public List<PassengerInfo> Passengers { get; set; }
        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long TotalPrice { get; set; }
        public bool BuyerIsPassenger { get; set; } = true;

        public bool IsPending { get; set; }
    }

    public class TeamPassengers
    {
        public Person Buyer { get; set; }
        public List<PassengerInfo> Passengers { get; set; }
    }

    [Route("/notify/tour/{TourId}/buyers/", "POST")]
    public class SendNotifyToTourBuyers : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }

    public class UpsertTeamValidator : AbstractValidator<UpsertTeam>
    {
        public UpsertTeamValidator()
        {
            RuleFor(t => t.TourId).NotEmpty().NotNull();
            RuleFor(t => t.Buyer.Id).NotNull().NotEmpty();
            RuleFor(t => t.Buyer.IsInfant).NotEqual(true);
            RuleFor(t => t.Buyer.IsUnder5).NotEqual(true);
        }
    }

    [Route("/tours/{TourId}/teams/{TeamId}", "POST")]
    public class UpsertTeam : IReturn<Team>
    {
        public Guid? TeamId { get; set; }
        public Guid TourId { get; set; }
        public Person Buyer { get; set; }
        public List<PassengerInfo> Passengers { get; set; }
        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long TotalPrice { get; set; }
    }

    public class UpdateTeamValidator : AbstractValidator<UpdateTeam>
    {
        public UpdateTeamValidator()
        {
            RuleFor(t => t.Team.Id).NotEmpty();
            RuleFor(t => t.Team.TourId).NotEmpty();
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

    public class UpdateTeamListValidator : AbstractValidator<PassengerReplacementTeamAccomplish>
    {
        public UpdateTeamListValidator()
        {
            RuleFor(x => x.Teams.Count).GreaterThan(0);
            RuleFor(x => x.Teams).SetCollectionValidator(new TeamListValidator());
        }
    }

    [Route("/team", "PUT")]
    public class UpdateTeam : IReturn
    {
        public Team Team { get; set; }
    }

    [Route("/tours/{OldTourId}/teams/list", "PUT")]
    public class PassengerReplacementTeamAccomplish : IReturnVoid
    {
        public Guid OldTourId { get; set; }
        public List<Team> Teams { get; set; }
    }

    [Route("/tours/{TourId}/teams", "GET")]
    public class GetTourTeams : QueryDb<Team>
    {
        public Guid TourId { get; set; }
    }

    [Route("/teams/{TeamId}/persons/", "GET")]
    public class GetPersonsOfTeam : IReturn<IList<PassengerInfo>>
    {
        public Guid TeamId { get; set; }
    }

    [Route("/tours/teams/{TeamId}", "DELETE")]
    public class DeleteTeam
    {
        [QueryDbField(Field = "Id")]
        public Guid TeamId { get; set; }
    }

}
