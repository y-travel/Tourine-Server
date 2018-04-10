using System.Collections.Generic;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Teams
{
    public class TeamLogics
    {
        public long CalculateTotalPrice(List<TeamMember> passengers,
            List<TourOption> tourOptions,
            long infantPrice, long basePrice)
        {
            long totalPrice = 0;
            foreach (var passenger in passengers)
            {
                //@TODO: calculate age from birthdate
                if (passenger.Person.IsUnder5)
                {
                    totalPrice += basePrice;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Bus) ? 0 : tourOptions.Find(x=> x.OptionType == OptionType.Bus).Price;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Room) ? 0 : tourOptions.Find(x => x.OptionType == OptionType.Room).Price;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Food) ? 0 : tourOptions.Find(x => x.OptionType == OptionType.Food).Price;
                }
                else if (passenger.Person.IsInfant)
                    totalPrice += infantPrice;
                else
                    totalPrice += basePrice;
            }
            return totalPrice;
        }
    }
}
