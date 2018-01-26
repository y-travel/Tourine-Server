﻿using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class PutPassengerValidatior : AbstractValidator<PutPassenger>
    {
        public PutPassengerValidatior()
        {
            RuleFor(p => p.Passenger.Id).NotEmpty();
            RuleFor(p => p.Passenger.BirthDate).NotEmpty();
            RuleFor(p => p.Passenger.Family).NotEmpty();
            RuleFor(p => p.Passenger.MobileNumber).NotEmpty();
            RuleFor(p => p.Passenger.Name).NotEmpty();
            RuleFor(p => p.Passenger.NationalCode).NotEmpty();
            RuleFor(p => p.Passenger.PassportExpireDate).NotEmpty();
            RuleFor(p => p.Passenger.PassportNo).NotEmpty();
            RuleFor(p => p.Passenger.AgencyId).NotEmpty();
        }
    }
}
