using System;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class Passenger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime PassportExpireDate { get; set; }
        public string PassportNo { get; set; }
        public bool Gender { get; set; }
        public PassengerType Type { get; set; } = PassengerType.Passenger;
    }
}
