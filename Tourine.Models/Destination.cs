using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    public class Destination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
    }
}
