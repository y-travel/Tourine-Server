using System;

namespace Tourine.ServiceInterfaces.Places
{
    public class Place
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
