using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
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
