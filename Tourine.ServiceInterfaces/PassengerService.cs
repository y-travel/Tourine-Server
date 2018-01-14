using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.ServiceInterfaces
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
            Db.DeleteById<Passenger>(deletePassenger.Id);
        }
    }
}
