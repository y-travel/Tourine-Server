using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    [Alias("PassengerList")] //@TODO should be removed
    public class Passenger
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public OptionType OptionType { get; set; }
        [Alias("HaveVisa")]//@TODO should be removed
        public bool HasVisa { get; set; }
        public bool PassportDelivered { get; set; }

        [References(typeof(Team))]
        public Guid TeamId { get; set; }
        [Reference]
        public Team Team { get; set; }
    }

    [Alias("PassengerList")]
    public class PassengerInfo
    {
        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }
        public OptionType OptionType { get; set; }

        [Ignore]
        public string OptionDisplay => Person.IsInfant || Person.IsUnder5 ? OptionType.GetDisplayTitle(true) : Strings.Adult;
        [Alias("HaveVisa")]
        public bool HasVisa { get; set; }
        public bool PassportDelivered { get; set; }
        public Guid TourId { get; set; }
        public Guid TeamId { get; set; }
    }

    public class TourPassengers
    {
        //@TODO omit
        public Tour Tour { get; set; }
        //@TODO omit
        public Person Leader { get; set; }
        public List<PassengerInfo> Passengers { get; set; }
    }

    public class TourPersonReport
    {
        public Tour Tour { get; set; }
        public Person Leader { get; set; }
        public List<Person> Passengers { get; set; }
    }

    [Route("/notify/tour/{TourId}/passengers/", "POST")]
    public class SendNotifyToTourPassengers : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }

    public class PutServiceForPassengerValidator : AbstractValidator<PutServiceForPassenger>
    {
        public PutServiceForPassengerValidator()
        {
            RuleFor(s => s.Passenger.Id).NotEmpty();
            RuleFor(s => s.Passenger.PersonId).NotEmpty();
            RuleFor(s => s.Passenger.TourId).NotEmpty();
            RuleFor(s => s.Passenger.OptionType).NotEmpty();
        }
    }

    [Route("/service", "PUT")]
    public class PutServiceForPassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }

    public class PostServiceForPassengerValidator : AbstractValidator<PostServiceForPassenger>
    {
        public PostServiceForPassengerValidator()
        {
            RuleFor(s => s.Passenger.PersonId).NotEmpty();
            RuleFor(s => s.Passenger.TourId).NotEmpty();
            RuleFor(s => s.Passenger.OptionType).NotEmpty();
        }
    }

    [Route("/service", "POST")]
    public class PostServiceForPassenger : IReturn<Passenger>
    {
        public Passenger Passenger { get; set; }
    }

    [Route("/tours/{TourId}/passengers/toTour/{DestTourId}/{AgencyId}", "POST")]
    public class PassengerReplacement : IReturn<TourTeammember>
    {
        public Guid TourId { get; set; }
        public Guid DestTourId { get; set; }
        public Guid AgencyId { get; set; }
        public List<PassengerInfo> Passengers { get; set; }
    }

    [Route("/tours/{TourId}/visa/{Have}")]
    public class GetTourVisa : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
        public bool? Have { get; set; } = true;
    }

    [Route("/tours/{TourId}/buyers", "GET")]
    public class GetTourBuyers : IReturn<IList<TourBuyer>>
    {
        public Guid TourId { get; set; }
    }

    [Route("/tours/{TourId}/tickets")]
    public class GetTourTicket : IReturn<TourPersonReport>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }

    [Route("/service/{TourId}")]
    public class GetServiceOfTour : QueryDb<Passenger, Person>
    {
        public Guid TourId { get; set; }
    }

}
