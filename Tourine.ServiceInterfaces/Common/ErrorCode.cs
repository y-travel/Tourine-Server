﻿namespace Tourine.ServiceInterfaces.Common
{
    public enum ErrorCode
    {
        NotFound = 1,
        TourNotFound = 2,
        TourHasPassenger = 3,
        TourHasBlock = 4,
        NotEnoughFreeSpace = 5,
        ExtraSpaceReserved = 6,
    }
}
