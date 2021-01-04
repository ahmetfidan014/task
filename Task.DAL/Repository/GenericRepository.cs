using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Task.DAL.Models;

namespace Task.DAL.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbSet;
        public int TabloTipi { get; set; }
        public string ProsedurAdi { get; set; }
        public object ParametreDegeri { get; set; }
        public string ParametreAdi { get; set; }


        public GenericRepository()
        {
            this.context = new TaskDBEntities();
            this.dbSet = context.Set<TEntity>();
        }
        public virtual TEntity GetLast()
        {
            var model = Get(Size: 1).FirstOrDefault();
            if (model == null)
            {
                return (TEntity)Activator.CreateInstance(typeof(TEntity));
            }
            return model;
        }
        public virtual int GetMax(string colName,string tableName)
        {
            return context.Database.SqlQuery<int>("SELECT " + colName + " FROM " + tableName + " WHERE " + colName + " = (SELECT max(" + colName + ") FROM " + tableName + ")").FirstOrDefault();
        }
        public virtual List<TEntity> Get(out int count, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "",
            string Sort = "Id", string Order = "desc", int Page = 1, int Size = 25)
        {
            try
            {
                if (HttpContext.Current.Request["Sort"] != null) { Sort = HttpContext.Current.Request["Sort"]; }
                if (HttpContext.Current.Request["Order"] != null) { Order = HttpContext.Current.Request["Order"]; }
                if (HttpContext.Current.Request["Page"] != null) { Page = Convert.ToInt32(HttpContext.Current.Request["Page"].ToString()); }
                if (HttpContext.Current.Request["Size"] != null) { Size = Convert.ToInt32(HttpContext.Current.Request["Size"].ToString()); }
            }
            catch (Exception)
            {
            }


            Page = Page <= 0 ? 1 : Page;
            Size = Size <= 1 ? 1 : Size;

            if (Order == "asc")
            {
                var query = ApplyOrder(filter, Sort, "OrderBy");
                count = query.Count();
                return query.Skip((Page - 1) * Size).Take(Size).AsEnumerable().ToList();
            }
            else
            {
                var query = ApplyOrder(filter, Sort, "OrderByDescending");
                count = query.Count();
                return query.Skip((Page - 1) * Size).Take(Size).AsEnumerable().ToList();
            }

        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "",
            string Sort = "Id", string Order = "desc", int Page = 1, int Size = 25)
        {
            try
            {
                if (HttpContext.Current.Request["Sort"] != null) { Sort = HttpContext.Current.Request["Sort"]; }
                if (HttpContext.Current.Request["Order"] != null) { Order = HttpContext.Current.Request["Order"]; }
                if (HttpContext.Current.Request["Page"] != null) { Page = Convert.ToInt32(HttpContext.Current.Request["Page"].ToString()); }
                if (HttpContext.Current.Request["Size"] != null) { Size = Convert.ToInt32(HttpContext.Current.Request["Size"].ToString()); }
            }
            catch (Exception)
            {
            }

            Page = Page <= 0 ? 1 : Page;
            Size = Size <= 1 ? 1 : Size;

            if (Order == "asc")
            {
                var query = ApplyOrder(filter, Sort, "OrderBy");
                return query.Skip((Page - 1) * Size).Take(Size).AsEnumerable().ToList();
            }
            else
            {
                var query = ApplyOrder(filter, Sort, "OrderByDescending");
                return query.Skip((Page - 1) * Size).Take(Size).AsEnumerable().ToList();
            }

        }


        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            var query = ApplyOrder(filter, "desc", "OrderByDescending");
            return query.AsEnumerable().ToList();
        }
        
        public virtual int Count(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);
            return query.Count();
        }

        IOrderedQueryable<TEntity> ApplyOrder(Expression<Func<TEntity, bool>> predicate, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            try
            {
                foreach (string prop in props)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            catch (Exception)
            {
                string[] prophata = { "Id" };
                foreach (string prop in prophata)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            IQueryable<TEntity> sour = GetQuery(predicate);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TEntity), type)
                    .Invoke(null, new object[] { sour, lambda });
            return (IOrderedQueryable<TEntity>)result;

        }

        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            if (TabloTipi == 0)
            {
                IQueryable<TEntity> query = dbSet;

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

                if (filter != null)
                    query = query.Where(filter);
                return query;
            }
            else
            {
                if (filter != null)
                {
                    var result = context.Database.SqlQuery<TEntity>(ProsedurAdi, ParametreDegeri).AsQueryable();

                    result = result.Where(filter);
                    return result;
                }
                else
                {
                    if (ParametreDegeri != null)
                    {
                        var param3 = new SqlParameter
                        {
                            ParameterName = ParametreAdi,
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Input,
                            Value = ParametreDegeri
                        };

                        var result = context.Database.SqlQuery<TEntity>("exec " + ProsedurAdi + " " + ParametreAdi, param3).AsQueryable();
                        return result;
                    }
                    else
                    {
                        var result = context.Database.SqlQuery<TEntity>(ProsedurAdi).AsQueryable();
                        return result;
                    }
                }
            }
        }

        public virtual TEntity GetById(object id)
        {
            var model = dbSet.Find(id);
            if (model == null)
            {
                return (TEntity)Activator.CreateInstance(typeof(TEntity));
            }
            return model;
        }


        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        public virtual void InsertOrUpdate(TEntity entity, int id)
        {
            if (id > 0)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            Save();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }           
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            Save();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            else
            {
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}