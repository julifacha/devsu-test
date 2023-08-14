using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using DevsuTest.Core.Interfaces;
using DevsuTest.Context.Contexts;

namespace DevsuTest.Repository.Base
{
    /// <summary>
    /// This class provides generic methods acting as a repository layer through the database EF context.
    /// </summary>
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DevsuDbContext context;
        protected const string PredicateEmpty = "False";

        public GenericRepository(DevsuDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (include != null)
                query = include(query);

            return query;
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await context.AddAsync(entity)).Entity;
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (include != null)
                query = include(query);

            if (!PredicateEmpty.Equals(predicate.Body.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return query.Where(predicate);

            return query;
        }

        public virtual async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }


        public virtual async Task<T?> GetByIdAsync<TId>(TId Id)
        {
            return await context.FindAsync<T>(Id);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity).Entity;
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}