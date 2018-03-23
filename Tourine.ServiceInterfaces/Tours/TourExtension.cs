using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public static class TourExtension
    {
        public static Guid Create(this Tour newTour, IDbConnection db, AuthSession session)
        {
            using (var trans = db.OpenTransaction())
            {
                var tour = new Tour();
                tour.PopulateWith(newTour);

                db.Insert(tour);
                db.SaveAllReferences(tour);
                foreach (var option in newTour.Options)
                {
                    var tmpOption = new TourOption();
                    tmpOption.PopulateWith(option);
                    tmpOption.TourId = tour.Id;
                    tmpOption.OptionStatus = option.OptionType.GetDefaultStatus();
                    db.Insert(tmpOption);
                }
                trans.Commit();
                return tour.Id;
            }
        }

        public static Guid Update(this Tour upsertTour, IDbConnection db)
        {
            var tour = db.SingleById<Tour>(upsertTour.Id);
            var tourDetail = db.SingleById<TourDetail>(upsertTour.TourDetail.Id);
            if (tour == null || tourDetail == null)
                throw HttpError.NotFound("");
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
                return tour.Id;
            }
        }
    }
}
