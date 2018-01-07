using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [References(typeof(Role))]
        public int RoleId { get; set; }
        [Reference]
        public Role Role { get; set; }
    }
}
