using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.AgencyCustomers;

namespace Tourine.ServiceInterfaces.Customers
{
    public class CustomerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetCustomer getCustomer)
        {
            if (getCustomer.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = getCustomer.Id }))
                throw HttpError.NotFound("");
            var item = Db.SingleById<Customer>(getCustomer.Id);
            return item;
        }

        [Authenticate]
        public object Get(GetCustomers getCustomers)
        {
            var query = AutoQuery.CreateQuery(getCustomers, Request.GetRequestParams());
            return AutoQuery.Execute(getCustomers, query);
        }

        [Authenticate]
        public object Post(CreateCustomer createCustomer)
        {

            createCustomer.Customer.Id = Guid.NewGuid();
            Db.Insert(createCustomer.Customer);
            Db.Insert(new AgencyCustomer
            {
                AgencyId = Session.Agency.Id,
                CustomerId = createCustomer.Customer.Id
            });
            return Db.SingleById<Customer>(createCustomer.Customer.Id);
        }

        [Authenticate]
        public void Put(UpdateCustomer updateCustomer)
        {
            if (updateCustomer.Customer.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = updateCustomer.Customer.Id }))
                throw HttpError.NotFound("");
            Db.Update(updateCustomer.Customer);
        }

        [Authenticate]
        public void Delete(DeleteCustomer deleteCustomer)
        {
            if (!Db.Exists<Customer>(new { Id = deleteCustomer.Id }))
                throw HttpError.NotFound("");
            var customer = Db.Single<AgencyCustomer>(x => x.CustomerId == deleteCustomer.Id && x.AgencyId == Session.Agency.Id);
            if (customer != null)
                Db.DeleteById<Customer>(deleteCustomer.Id);
            else
                throw HttpError.NotFound("");
        }
    }

}

