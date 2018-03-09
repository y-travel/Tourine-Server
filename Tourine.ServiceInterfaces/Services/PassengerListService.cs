using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Services
{
    public class PassengerListService : AppService
    {
        public IAutoQueryDb AutoQueryDb { get; set; }
        public object Post(PostServiceForPassenger forPassenger)
        {
            forPassenger.PassengerList.Id = Guid.NewGuid();
            Db.Insert(forPassenger.PassengerList);
            return Db.SingleById<PassengerList>(forPassenger.PassengerList.Id);
        }

        public void Put(PutServiceForPassenger forPassenger)
        {
            if (!Db.Exists<PassengerList>(new { Id = forPassenger.PassengerList.Id }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = forPassenger.PassengerList.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(new { Id = forPassenger.PassengerList.TourId }))
                throw HttpError.NotFound("");
            Db.Update(forPassenger.PassengerList);
        }

        public object Get(GetServiceOfTour serviceOfTour)
        {
            var query = AutoQueryDb.CreateQuery(serviceOfTour, Request.GetRequestParams());
            return AutoQueryDb.Execute(serviceOfTour, query);
        }
    }
}
