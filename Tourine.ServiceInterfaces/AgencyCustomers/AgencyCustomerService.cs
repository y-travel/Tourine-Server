using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Customers;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    public class AgencyCustomerService : AppService
    {
        public IAutoQueryDb AutoQueryDb { get; set; }

        [Authenticate]
        public object Get(GetCustomerOfAgency agency)
        {
            if (agency.AgencyId == null || agency.AgencyId == Guid.Empty)
                agency.AgencyId = Session.Agency.Id;
            if (!Db.Exists<Agency>(new { Id = agency.AgencyId }))
                throw HttpError.NotFound("");
            var items = AutoQueryDb.CreateQuery(agency, Request.GetRequestParams());
            return AutoQueryDb.Execute(agency, items);
        }

        [Authenticate]
        public object Post(AddCustomerToAgency toAgency)
        {
            if (toAgency.AgencyId == null || toAgency.AgencyId == Guid.Empty)
                toAgency.AgencyId = Session.Agency.Id;
            if (!Db.Exists<Agency>(new { Id = toAgency.AgencyId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = toAgency.CustomerId }))
                throw HttpError.NotFound("");
            Db.Insert(new AgencyCustomer
            {
                AgencyId = toAgency.AgencyId.Value,
                CustomerId = toAgency.CustomerId
            });
            return Db.Single<AgencyCustomer>(x =>
                x.AgencyId == toAgency.AgencyId.Value && x.CustomerId == toAgency.CustomerId);
        }

        [Authenticate]
        public void Put(UpdateCustomerToAgency toAgency)
        {
            if (!Db.Exists<Agency>(new { Id = toAgency.AgencyId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = toAgency.CustomerId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<AgencyCustomer>(new { Id = toAgency.Id }))
                throw HttpError.NotFound("");
            Db.Update(new AgencyCustomer
            {
                Id = toAgency.Id,
                AgencyId = toAgency.AgencyId,
                CustomerId = toAgency.CustomerId
            });
        }
    }
}
