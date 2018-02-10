using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces
{
    public class PriceDetail
    { 
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Value { get; set; }
        public string Title { get; set; }

        [References(typeof(Currency))]
        public int CurrencyId { get; set; }
        [Reference]
        public Currency Currency { get; set; }

        [References(typeof(Tour))]
        public int TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }
    }
}
