using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;
using Tourine.Models.DatabaseModels;
using Tourine.Models.ServiceModels;

namespace Tourine.ServiceInterfaces
{
    public class CustomerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetCustomer getCustomer)
        {
            if (getCustomer.Id == null)
                throw HttpError.NotFound("");
            var item = Db.SingleById<Customer>(getCustomer.Id);
            if (item.Id != getCustomer.Id)
                throw HttpError.NotFound("");
            return item;

        }

        public object Get(GetCustomers customers)
        {
            var query = AutoQuery.CreateQuery(customers, Request.GetRequestParams());
            return AutoQuery.Execute(customers, query);
        }

        public object Post(PostCustomer postCustomer)
        {
            //TODO validate Customer filels
            postCustomer.Customer.Id = Guid.NewGuid();
            Db.Insert(postCustomer.Customer);
            var item = Db.SingleById<Customer>(postCustomer.Customer.Id);
            return item;
        }

        public object Put(PutCustomer putCustomer)
        {
            if (putCustomer.Customer.Id == null)
                throw HttpError.NotFound("");
            Db.Update(putCustomer.Customer);
            var item = Db.SingleById<Customer>(putCustomer.Customer.Id);
            if (item.Id != putCustomer.Customer.Id)
                throw HttpError.NotFound("");

            return item;
        }

        public object Delete(DeleteCustomer deleteCustomer)
        {
            var item = Db.SingleById<Customer>(deleteCustomer.Id);
            if (!item.Id.Equals(deleteCustomer.Id))
                throw HttpError.NotFound("");
            Db.DeleteById<Customer>(deleteCustomer.Id);
            return item;
        }
    }

}

