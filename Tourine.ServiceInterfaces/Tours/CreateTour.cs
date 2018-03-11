using System.Collections.Generic;
using ServiceStack;
using ServiceStack.FluentValidation.Attributes;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/", "POST")]
    [Validator(typeof(CreateTourValidator))]
    [Api("ww",2,IsRequired = true)]
    public class CreateTour : IReturn<Tour>
    {
        public int Capacity { get; set; }
        public int BasePrice { get; set; }
        public int InfantPrice { get; set; }
        public List<TourOption> Options { get; set; }
        public TourDetail TourDetail { get; set; }
    }
}
