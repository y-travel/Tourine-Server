using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class PriceDetail
    {
        [AutoIncrement]
        public Guid Id { get; set; }
        public int Value { get; set; }

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
