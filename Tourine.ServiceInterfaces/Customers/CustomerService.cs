using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Customers
{
    public class CustomerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetCustomer getCustomer)
        {
            if (getCustomer.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = getCustomer.Id }))
                throw HttpError.NotFound("");
            var item = Db.SingleById<Customer>(getCustomer.Id);
            return item;
        }

        public object Get(GetCustomers getCustomers)
        {
            var query = AutoQuery.CreateQuery(getCustomers, Request.GetRequestParams());
            return AutoQuery.Execute(getCustomers, query);
        }

        public void Post(PostCustomer postCustomer)
        {
            Db.Insert(postCustomer.Customer);
        }

        public void Put(PutCustomer putCustomer)
        {
            if (putCustomer.Customer.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Customer>(new { Id = putCustomer.Customer.Id }))
                throw HttpError.NotFound("");
            Db.Update(putCustomer.Customer);
        }

        public void Delete(DeleteCustomer deleteCustomer)
        {
            if (!Db.Exists<Customer>(new { Id = deleteCustomer.Id }))
                throw HttpError.NotFound("");
            Db.DeleteById<Customer>(deleteCustomer.Id);
        }
    }

}

