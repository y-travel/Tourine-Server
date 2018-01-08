using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.FluentValidation.Validators;
using ServiceStack.OrmLite;
using Tourine.Models;

namespace Tourine.ServiceInterfaces
{
    public class ReagentService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public object Post(PostReagent PostReagent)
        {
            //TODO validate Reagent fields value
            PostReagent.reagent.Id = Guid.NewGuid();
            Db.Insert(PostReagent.reagent);
            return Db.SingleById<Reagent>(PostReagent.reagent.Id);
        }

        public object Put(PutReagent PutReagent)
        {
            //TODO validate Reagent fields value
            var item = Db.SingleById<Reagent>(PutReagent.reagent.Id);
            if (item == null)
                throw HttpError.NotFound("");
            Db.Update(PutReagent.reagent);
            return Db.SingleById<Reagent>(PutReagent.reagent.Id);
        }

        public object Get(GetReagent GetReagent)
        {
            var query = AutoQuery.CreateQuery(GetReagent, Request.GetRequestParams());
            return AutoQuery.Execute(GetReagent, query);
        }

        public object Delete(DeleteReagent delId)
        {
            var item = Db.SingleById<Reagent>(delId.Id);
            if (item == null)
                throw HttpError.NotFound("");

            Db.DeleteById<Reagent>(delId.Id);
            return item;
        }
    }
}
