using System;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;

namespace Tourine.Test
{
    public class InsertHelper<T>
    {
        public T Result { get; }

        private IDbConnection Db { get; }

        public InsertHelper(IDbConnection db, T result)
        {
            Db = db;
            Result = result;
            Save();
        }

        public InsertHelper<T2> Insert<T2>(T2 entity)
        {
            return new InsertHelper<T2>(Db, entity);
        }

        public InsertHelper<T2> Insert<T2>(Func<T, T2> expression)
        {
            return new InsertHelper<T2>(Db, expression(Result));
        }

        protected void Save()
        {
            var autoIncrementField = Result.GetType().GetModelMetadata().FieldDefinitions.FirstOrDefault(x => x.AutoIncrement);
            Db.Insert(Result);
            autoIncrementField?.SetValueFn(Result, (int)Db.LastInsertId());
        }
    }
}