using System;

namespace Tourine.ServiceInterfaces.Agencies
{
    public class Agency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
