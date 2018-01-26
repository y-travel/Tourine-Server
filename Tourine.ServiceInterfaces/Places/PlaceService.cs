using System;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Tourine.ServiceInterfaces.Places
{
    public class PlaceService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetPlaces getPlaces)
        {
            var query = AutoQuery.CreateQuery(getPlaces, Request.GetRequestParams());
            return AutoQuery.Execute(getPlaces, query);
        }

        public void Post(PostPlace place)
        {
            Db.Insert(place.Place);
        }

        public void Put(PutPlace place)
        {

            if (!Db.Exists<Place>(new Place { Id = place.Place.Id }))
                throw HttpError.NotFound("");
            Db.Update(place.Place);

        }
    }
}
