using System;

namespace Tourine.ServiceInterfaces
{
    [Flags]
    public enum PersonType
    {
        Passenger = 1,
        Customer = 2,
        Leader = 4
    }
}