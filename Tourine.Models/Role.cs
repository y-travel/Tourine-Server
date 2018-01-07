using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
