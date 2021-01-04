using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task.DAL
{
    public interface IRepository<T> where T : class
    {
        T GetLast();
        int GetMax(string colName, string tableName);
        List<T> Get(out int count, Expression<Func<T, bool>> filter = null,
            string includeProperties = "",
            string Sort = "Id", string Order = "desc", int Page = 1, int Size = 25);
        List<T> Get(Expression<Func<T, bool>> filter = null,
            string includeProperties = "",
            string Sort = "Id", string Order = "desc", int Page = 1, int Size = 25);
        List<T> GetAll(Expression<Func<T, bool>> filter = null,
            string includeProperties = "");
        int Count(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        //IOrderedQueryable<T> ApplyOrder(Expression<Func<T, bool>> predicate, string property, string methodName);
        //IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        T GetById(object id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        void InsertOrUpdate(T entity, int id);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        void Save();
        //void Dispose(bool disposing);
        void Dispose();
    }
}
