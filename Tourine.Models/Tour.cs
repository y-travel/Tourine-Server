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
        public long Id { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        [Ignore]
        public long Price { get; set; }
        [Ignore]
        public string Destination { get; set; }
    }
}
