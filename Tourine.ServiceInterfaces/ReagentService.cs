using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.FluentValidation.Validators;
using ServiceStack.OrmLite;
using Tourine.Models;
using Tourine.Models.DatabaseModels;
using Tourine.Models.ServiceModels;

namespace Tourine.ServiceInterfaces
{
    public class ReagentService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public object Post(PostReagent postReagent)
        {
            //TODO validate Reagent fields value
            postReagent.reagent.Id = Guid.NewGuid();
            Db.Insert(postReagent.reagent);
            return Db.SingleById<Reagent>(postReagent.reagent.Id);
        }

        public object Put(PutReagent putReagent)
        {
            //TODO validate Reagent fields value
            var item = Db.SingleById<Reagent>(putReagent.reagent.Id);
            if (item == null)
                throw HttpError.NotFound("");
            Db.Update(putReagent.reagent);
            return Db.SingleById<Reagent>(putReagent.reagent.Id);
        }

        public object Get(GetReagent getReagent)
        {
            var query = AutoQuery.CreateQuery(getReagent, Request.GetRequestParams());
            return AutoQuery.Execute(getReagent, query);
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
