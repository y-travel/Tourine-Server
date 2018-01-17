using System;

namespace Tourine.ServiceInterfaces.Customers
{
    public class Customer
    {
        public Guid Id { set; get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string Phone { get; set; }
    }
}
