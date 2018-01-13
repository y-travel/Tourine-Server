using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class PriceDetail
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        [References(typeof(Currency))]
        public int CurrencyId { get; set; }
        [Reference]
        public Currency Currency { get; set; }
    }
}
