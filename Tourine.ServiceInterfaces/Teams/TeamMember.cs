using System;
using System.Collections.Generic;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Teams
{
    public class TeamMember
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public List<PersonIncome> PersonIncomes { get; set; }
        public bool VisaDelivered { get; set; }
        public bool PassportDelivered { get; set; }
    }

    public class PersonIncome
    {
        public OptionType OptionType { get; set; }
        public long ReceivedMoney { get; set; }
        public IncomeStatus IncomeStatus { get; set; }
        public double CurrencyFactor { get; set; } = 1 ;
    }
}
