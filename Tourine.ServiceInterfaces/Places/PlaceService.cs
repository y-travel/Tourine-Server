using System;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Tourine.ServiceInterfaces.Places
{
    public class PlaceService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetPlaces getPlaces)
        {
            var query = AutoQuery.CreateQuery(getPlaces, Request.GetRequestParams());
            return AutoQuery.Execute(getPlaces, query);
        }

        [Authenticate]
        public void Post(PostPlace place)
        {
            Db.Insert(place.Place);
        }

        [Authenticate]
        public void Put(PutPlace place)
        {

            if (!Db.Exists<Place>(new  { Id = place.Place.Id }))
                throw HttpError.NotFound("");
            Db.Update(place.Place);

        }
    }
}
