using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourine.Models
{
    public class Reagent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string AgencyName { get; set; }
        public string MobileNo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
