using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public static class TourExtension
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
                    tmpOption.OptionStatus = option.OptionType.GetDefaultStatus();
                    db.Insert(tmpOption);
                }
                trans.Commit();
            }
            return tour;
        }

        public static Tour Update(this Tour upsertTour, IDbConnection db, AuthSession session)
        {
            var tour = db.SingleById<Tour>(upsertTour.Id);
            var tourDetail = db.SingleById<TourDetail>(upsertTour.TourDetail.Id);
            if (tour == null || tourDetail == null)
                throw HttpError.NotFound($"tourId:{upsertTour.Id}tourDetailId:{upsertTour.TourDetail.Id}");
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
    }
}
