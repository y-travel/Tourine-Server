using System;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public static class TourExtensions
    {
        public static Tour Create(this Tour newTour, IDbConnection db, AuthSession session)
        {
            var tour = new Tour();

            using (var trans = db.OpenTransaction())
            {
                tour.SafePopulate(newTour);
                tour.AgencyId = session.Agency.Id;
                db.Insert(tour);
                db.SaveAllReferences(tour);
                foreach (var option in newTour.Options)
                {
                    var tmpOption = new TourOption();
                    tmpOption.SafePopulate(option);
                    tmpOption.TourId = tour.Id;
                    //@TODO should be got from user
                    tmpOption.OptionStatus = option.OptionType.GetDefaultStatus();
                    db.Insert(tmpOption);
                }
                trans.Commit();
            }
            return tour;
        }
        public static Tour CreateBlock(this Tour block, IDbConnection db)
        {
            var newBlock = new Tour();
            newBlock.SafePopulate(block);
            using (var trans = db.OpenTransaction())
            {
                block.CheckFreeSpace(db, block.Capacity);
                db.Insert(newBlock);
                foreach (var option in block.Options)
                {
                    var tmpOption = new TourOption();
                    tmpOption.SafePopulate(option);
                    tmpOption.TourId = newBlock.Id;
                    //@TODO should be got from user
                    tmpOption.OptionStatus = option.OptionType.GetDefaultStatus();
                    db.Insert(tmpOption);
                }
                trans.Commit();
            }
            return newBlock;
        }
        public static Tour Update(this Tour upsertTour, IDbConnection db, AuthSession session)
        {
            var tour = db.SingleById<Tour>(upsertTour.Id);
            var tourDetail = db.SingleById<TourDetail>(upsertTour.TourDetail.Id);
            if (tour == null || tourDetail == null)
                throw HttpError.NotFound($"tourId:{upsertTour.Id} tourDetailId:{upsertTour.TourDetail.Id}");
            using (var dbTrans = db.OpenTransaction())
            {
                tour.SafePopulate(upsertTour);
                tour.AgencyId = session.Agency.Id;
                tour.TourDetailId = tourDetail.Id;
                db.Update(tour);
                tourDetail.SafePopulate(upsertTour.TourDetail);
                db.Update(tourDetail);
                foreach (var option in upsertTour.Options)
                {
                    option.TourId = tour.Id;//to keep db integration
                    db.Update(option);
                }
                dbTrans.Commit();
            }
            //update return value
            tour.TourDetail = tourDetail;
            tour.Options = upsertTour.Options;
            return tour;
        }

        public static Tour UpdateBlock(this Tour block, IDbConnection db)
        {
            var oldBlock = db.SingleById<Tour>(block.Id);
            if (oldBlock == null)
                throw HttpError.NotFound("Block not found!");
            if (!db.Exists<Agency>(x => x.Id == block.AgencyId))
                throw HttpError.NotFound("Agency not found!");
            oldBlock.CheckFreeSpace(db, block.Capacity);
            using (var trans = db.OpenTransaction())
            {
                oldBlock.SafePopulate(block);
                db.Update(oldBlock);
                foreach (var option in block.Options)
                {
                    option.TourId = block.Id;//to keep db integration
                    db.Update(option);
                }
                trans.Commit();
            }

            oldBlock.Options = block.Options;
            return oldBlock;
        }
        public static bool Delete(this Tour req, IDbConnection db)
        {
            using (var trans = db.OpenTransaction())
            {
                var tour = db.SingleById<Tour>(req.Id);
                if (tour == null)
                    throw HttpError.NotFound("TourId Not Found");
                if (tour.IsBlock)
                    db.Delete<Tour>(x => x.Id == tour.Id);
                else//@TODO find a solution: if a block set another detailId will be remained
                    db.Delete<TourDetail>(x => x.Id == req.TourDetailId);
                trans.Commit();
                return true;
            }
        }

        public static void CheckFreeSpace(this Tour tour, IDbConnection db, int capacity)
        {
            if (!tour.IsBlock)
                return;
            var parentTour = db.SingleById<Tour>(tour.ParentId);
            if (parentTour == null)
                throw HttpError.NotFound("parent tour");
            var blocksCapacity = parentTour.GetBlocksCapacity(db);
            var parentPassengerCount = parentTour.GetCurrentPassengerCount(db);

            if (parentTour.Capacity - blocksCapacity - parentPassengerCount +
                (tour.Id != Guid.Empty ? tour.Capacity : 0) < capacity
            )
                throw HttpError.Forbidden("There is no free space!");
        }

        public static int GetBlocksCapacity(this Tour tour, IDbConnection db) =>
            db.Scalar<Tour, int>(
                t => Sql.Sum(t.Capacity),
                predicate: t => t.ParentId == tour.Id
            );

        public static int GetCurrentPassengerCount(this Tour tour, IDbConnection db) =>
            db.Scalar<PassengerList, int>(
                x => Sql.CountDistinct(x.PersonId),
                x => x.TourId == tour.Id
            );

        public static bool IsPassengerExist(this Tour tour, Guid forPersonId, IDbConnection db) =>
            db.Exists<PassengerList>(x => x.TourId == tour.Id && x.PersonId == forPersonId);

        public static Tour ReservePendingBlock(this Tour block, int capacity, Guid toAgency, IDbConnection db)
        {
            var newBlock = new Tour();
            var options = db.Select(db.From<TourOption>().Where(to => to.TourId == block.Id));

            newBlock.SafePopulate(block);
            newBlock.Status = TourStatus.Creating;
            newBlock.ParentId = block.Id;
            newBlock.Capacity = capacity;
            newBlock.AgencyId = toAgency;
            newBlock.TourDetailId = block.TourDetailId;

            var createNewBlock = block.AgencyId != toAgency;
            if (createNewBlock)
            {
                db.Insert(newBlock);
                foreach (OptionType option in Enum.GetValues(typeof(OptionType)))
                {
                    if (option == OptionType.Empty) continue;
                    var newOption = new TourOption
                    {
                        TourId = newBlock.Id,
                        OptionType = option,
                        Price = options.Find(x => x.OptionType == option).Price,
                        OptionStatus = option.GetDefaultStatus()
                    };
                    db.Insert(newOption);
                }
            }
            else
                newBlock.Id = block.Id;
            return newBlock;
        }

        public static void ClearPending(this Tour tour, IDbConnection db)
        {
            db.Delete<Team>(x => x.TourId == tour.Id && x.IsPending);
            //passengerList table updated because of cascade delete
        }
    }
}
