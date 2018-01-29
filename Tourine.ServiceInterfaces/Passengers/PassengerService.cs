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
            if (!Db.Exists<Passenger>(new { Id = deletePassenger.Id }))
                throw HttpError.NotFound("");
            Db.DeleteById<Passenger>(deletePassenger.Id);
        }

        public object Get(GetPassengerFromNc fromNc)
        {
            if (!Db.Exists<Passenger>(new { NationalCode = fromNc.NationalCode }))
                throw HttpError.NotFound("");
            return Db.Single<Passenger>(new { NationalCode = fromNc.NationalCode });
        }

        public object Get(FindPassengerInAgency passengers)
        {
            if (!Db.Exists<Agency>(new Agency() { Id = passengers.AgencyId }))
                throw HttpError.NotFound("");
            var item = Db.From<Passenger>().Where(p => (p.AgencyId == passengers.AgencyId) && (p.Name.Contains(passengers.Str) || p.Family.Contains(passengers.Str) || p.MobileNumber.Contains(passengers.Str) || p.NationalCode.Contains(passengers.Str) || p.PassportNo.Contains(passengers.Str)));
            return AutoQuery.Execute(passengers, item);
        }

    }
}
