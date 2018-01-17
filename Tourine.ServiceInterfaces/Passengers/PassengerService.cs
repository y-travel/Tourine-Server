using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class PassengerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public void Post(PostPassenger postPassenger)
        {
            Db.Insert(postPassenger.Passenger);
        }

        public void Put(PutPassenger putPassenger)
        {
            if (!Db.Exists<Passenger>(new { Id = putPassenger.Passenger.Id }))
                throw HttpError.NotFound("");
            Db.Update(putPassenger.Passenger);
        }

        public object Get(GetPassengers getPassenger)
        {
            var query = AutoQuery.CreateQuery(getPassenger, Request.GetRequestParams());
            return AutoQuery.Execute(getPassenger, query);
        }

        public void Delete(DeletePassenger deletePassenger)
        {
            if (deletePassenger.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Passenger>(new  { Id = deletePassenger.Id }))
                throw HttpError.NotFound("");
            Db.DeleteById<Passenger>(deletePassenger.Id);
        }
    }
}
