using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourine.ServiceInterfaces.Tours
{
    public class TourOption
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public OptionType OptionType { get; set; }
        public long Price { get; set; }
        public OptionStatus Status { get; set; }
        public Guid TourId { get; set; }
    }

    public enum OptionStatus
    {
        Limited = 1,
        Unlimited = 2,
    }
}
