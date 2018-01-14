using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Customer
    {
        [AutoIncrement]
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string Phone { get; set; }
    }
}
