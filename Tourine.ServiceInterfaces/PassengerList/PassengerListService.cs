using System;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
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

        public void Put(ChangePassengerFromBlock changePassengerFromBlock)
        {
            try
            {
                if (!Db.Exists<Passenger>(new {Id = changePassengerFromBlock.PassengerBlock.PassengerId}))
                    throw HttpError.NotFound("");
                if (!Db.Exists<Block>(new {Id = changePassengerFromBlock.PassengerBlock.BlockId}))
                    throw HttpError.NotFound("");
                Db.Update(changePassengerFromBlock.PassengerBlock);
            }
            catch (Exception e)
            {
                e.PrintDump();
            }
        }
    }
}
