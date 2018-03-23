using System;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
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
                tour.PopulateFromPropertiesWithoutAttribute(newTour,typeof(NotPopulateAttribute));

                db.Insert(tour);
                db.SaveAllReferences(tour);
                foreach (var option in newTour.Options)
                {
                    var tmpOption = new TourOption();
                    tmpOption.PopulateFromPropertiesWithoutAttribute(option,typeof(NotPopulateAttribute));
                    tmpOption.TourId = tour.Id;
                    tmpOption.OptionStatus = option.OptionType.GetDefaultStatus();
                    db.Insert(tmpOption);
                }
                trans.Commit();
            }
            return tour;
        }

        public static Tour Update(this Tour upsertTour, IDbConnection db)
        {
            var tour = db.SingleById<Tour>(upsertTour.Id);
            var tourDetail = db.SingleById<TourDetail>(upsertTour.TourDetail.Id);
            if (tour == null || tourDetail == null)
                throw HttpError.NotFound($"tourId:{upsertTour.Id}tourDetailId:{upsertTour.TourDetail.Id}");
            using (var dbTrans = db.OpenTransaction())
            {
                tour.PopulateWith(upsertTour);
                tourDetail.PopulateWith(upsertTour.TourDetail);
                foreach (var option in upsertTour.Options)
                {
                    option.TourId = tour.Id;//to keep db integration
                    db.Update(option, where: x => x.TourId == tour.Id && x.OptionType == option.OptionType);
                }
                dbTrans.Commit();
            }
            return tour;

        }
    }
}
