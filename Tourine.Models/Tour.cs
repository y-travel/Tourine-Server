using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }

        [References(typeof(PriceDetail))]
        public Guid PriceDetailId { get; set; }
        [Reference]
        public PriceDetail PriceDetail { get; set; }

        [References(typeof(Destination))]
        public Guid DestinationId { get; set; }
        [Reference]
        public Destination Destination { get; set; }

        [References(typeof(Place))]
        public Guid PlaceId { get; set; }
        [Reference]
        public Place Place { get; set; }
        
        [References(typeof(Status))]
        public int StatusId { get; set; }
        [Reference]
        public Status Status { get; set; }

        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public bool IsFlight { get; set; }
        public int AdultCount { get; set; }
        [Ignore]
        public int InfantCount { get; set; }
        public int AdultMinPrice { get; set; }
        public int InfantPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; }
        public int FoodPrice { get; set; }
    }
}
