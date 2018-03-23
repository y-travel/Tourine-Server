using System;

namespace Tourine.ServiceInterfaces.Persons
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string EnglishName { get; set; }
        public string EnglishFamily { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? PassportExpireDate { get; set; }
        public DateTime? VisaExpireDate { get; set; }
        public string PassportNo { get; set; }
        public bool Gender { get; set; }
        public PersonType Type { get; set; } = PersonType.Passenger;
        public string SocialNumber { get; set; }
        public long ChatId { get; set; }

        public bool IsUnder5 { get; set; }
        public bool IsInfant { get; set; }
    }
}
