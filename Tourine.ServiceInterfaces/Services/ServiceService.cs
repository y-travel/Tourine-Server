using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Services
{
    public class ServiceService : AppService
    {
        public IAutoQueryDb AutoQueryDb { get; set; }
        public object Post(PostServiceForPassenger forPassenger)
        {
            forPassenger.Service.Id = Guid.NewGuid();
            Db.Insert(forPassenger.Service);
            return Db.SingleById<Service>(forPassenger.Service.Id);
        }

        public void Put(PutServiceForPassenger forPassenger)
        {
            if (!Db.Exists<Service>(new { Id = forPassenger.Service.Id }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = forPassenger.Service.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(new { Id = forPassenger.Service.TourId }))
                throw HttpError.NotFound("");
            Db.Update(forPassenger.Service);
        }

        public object Get(GetServiceOfTour serviceOfTour)
        {
            var query = AutoQueryDb.CreateQuery(serviceOfTour, Request.GetRequestParams());
            return AutoQueryDb.Execute(serviceOfTour, query);
        }
    }
}
