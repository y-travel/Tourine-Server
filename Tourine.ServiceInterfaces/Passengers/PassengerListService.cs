using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
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

        public object Post(PassengerReplacement req)
        {
            var srcTour = Db.SingleById<Tour>(req.TourId);
            if (srcTour == null)
                throw HttpError.NotFound("");
            var desTour = Db.SingleById<Tour>(req.DestTourId);
            if (desTour == null)
                throw HttpError.NotFound("");

            var teamLogic = new TeamLogic(Db);

            //calculate destination tour free space
            //@TODO: should be calculate with trigger in db
            return null;
        }
    }

    public class TourTeammember
    {
        public bool isTeam { get; set; }
        public Guid Id { get; set; }
        public long BasePrice { get; set; }
        public long InfantPrice { get; set; }
        public long FoodPrice { get; set; }
        public long BusPrice { get; set; }
        public long RoomPrice { get; set; }
        public Agency Agency { get; set; }
        public List<Team> Teams { get; set; }

    }
}
