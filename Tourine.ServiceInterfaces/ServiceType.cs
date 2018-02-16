using System;

namespace Tourine.ServiceInterfaces
{
    [Flags]
    public enum ServiceType : long
    {
        Hotel = 1,
        Bus = 2,
        Food = 4,
        Room = 8
    }
}
