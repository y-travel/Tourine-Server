using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    [Route("/notify/tour/{TourId}/leader", "POST")]
    public class SendNotifyToTourLeader : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }

    public class UpsertTourValidator : AbstractValidator<UpsertTour>
    {
        public UpsertTourValidator()
        {
            RuleFor(t => t.Capacity).NotEmpty().NotNull();
            RuleFor(t => t.BasePrice).NotEmpty().NotNull();
            When(t => !t.IsBlock, () =>
            {
                RuleFor(t => t.TourDetail.Duration).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
                RuleFor(t => t.TourDetail.DestinationId).NotEmpty().NotNull();
                RuleFor(t => t.TourDetail.StartDate).NotEmpty().NotNull();
                RuleFor(t => t.TourDetail.PlaceId).NotEmpty().NotNull();
            });
            RuleFor(t => t.ParentId).NotEqual(x => x.Id);
            RuleFor(t => t.Options.Count).Equal(3);
        }
    }

    [Route("/tours/{Id}", "POST")]
    public class UpsertTour : IReturn<Tour>
    {
        //Shared Properties
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public int BasePrice { get; set; }
        public int InfantPrice { get; set; }
        public List<TourOption> Options { get; set; }
        public bool IsBlock { get; set; }

        //Tour Properties
        public TourDetail TourDetail { get; set; }

        //Block Properties
        public Guid? ParentId { get; set; }
        public Guid? AgencyId { get; set; }

    }

    public class PassengerReplacementTourAccomplishValidator : AbstractValidator<PassengerReplacementTourAccomplish>
    {
        public PassengerReplacementTourAccomplishValidator()
        {
            RuleFor(x => x.OldTourId).NotNull().NotEmpty();
            RuleFor(x => x.TourId).NotNull().NotEmpty();
            RuleFor(x => x.BasePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.InfantPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.FoodPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.RoomPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.BusPrice).NotEmpty().NotNull().GreaterThan(0);
        }
    }

    [Route("/tours/{TourId}/{OldTourId}/accomplish","PUT")]
    public class PassengerReplacementTourAccomplish : IReturnVoid
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
        public Guid OldTourId { get; set; }
        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long BusPrice { get; set; }
        public long FoodPrice { get; set; }
        public long RoomPrice { get; set; }
    }

    [Route("/tours", "GET")]
    public class GetTours : QueryDb<Tour>, IJoin<Tour, TourDetail>
    {
    }

    [Route("/tours/{TourId}/passengers/", "GET")]
    public class GetTourPassengers : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
        public bool? HasVisa { get; set; }
    }

    [Route("/tours/{TourId}/freespace","GET")]
    public class GetTourFreeSpace : IReturn<string>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }

    [Route("/tours/{TourId}/agencies/{LoadChilds}","GET")]
    public class GetTourAgency : IReturn<IList<Tour>>
    {
        public Guid TourId { get; set; }
        public bool LoadChild { get; set; } = false;
    }

    [Route("/tours/{ID}", "GET")]
    public class GetTour : IGet
    {
        public Guid Id { get; set; }
    }

    [Route("/tours/root","GET")]
    public class GetRootTours : QueryDb<Tour> , IJoin<Tour,TourDetail>
    {
    }

    [Route("/tours/{TourId}/blocks", "GET")]
    public class GetBlocks : QueryDb<Tour>
    {
        [QueryDbField(Field = "Tour.ParentId")]
        public Guid TourId { get; set; }
    }

    [Route("/tours/{Id}","DELETE")]
    public class DeleteTour : IReturn
    {
        public Guid Id { get; set; }
    }

    public class Tour
    {
        [NotPopulate]
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Capacity { get; set; }
        public int FreeSpace { get; set; }

        public long BasePrice { get; set; }

        [References(typeof(Tour))]
        public Guid? ParentId { get; set; }

        [Reference]
        public Tour Parent { get; set; }

        [NotPopulate]
        public string Code { get; set; }

        [NotPopulate]
        public TourStatus Status { get; set; } = TourStatus.Created;//@TODO set to creating

        [NotPopulate]
        [References(typeof(TourDetail))]
        public Guid? TourDetailId { get; set; }

        [Reference]
        public TourDetail TourDetail { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        public long InfantPrice { get; set; }

        [NotPopulate]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Ignore]
        public List<TourOption> Options { get; set; }

        [Ignore]
        public bool IsBlock => ParentId != null && ParentId != Guid.Empty;

    }
}
