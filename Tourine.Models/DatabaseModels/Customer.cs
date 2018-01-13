using System;

namespace Tourine.Models.DatabaseModels
{
    public class Customer
    {
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime PassportExpireDate { get; set; }
        public string PassportNo { get; set; }
    }
}
