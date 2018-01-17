using System;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class Destination
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }    
    }
}
