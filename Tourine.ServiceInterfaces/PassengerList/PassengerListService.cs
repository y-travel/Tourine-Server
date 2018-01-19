using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Blocks;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.ServiceInterfaces.PassengerList
{
    public class PassengerListService : AppService
    {
        public void Post(AddPassengerToBlock toBlock)
        {
            Db.Insert(toBlock.PassengerList);
        }

        public void Delete(RemovePassengerFromBlock passengerFromBlock)
        {
            if (!Db.Exists<Passenger>(new { Id = passengerFromBlock.PId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Block>(new { Id = passengerFromBlock.BId }))
                throw HttpError.NotFound("");
            Db.Delete<PassengerList>(x => x.PassengerId == passengerFromBlock.PId && x.BlockId == passengerFromBlock.BId);
        }
    }
}
