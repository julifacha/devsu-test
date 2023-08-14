using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DevsuTest.Core.Interfaces
{
    /// <summary>
    /// Interfaz para metodos de uso comun de Entity Framework
    /// </summary>
    /// <typeparam name="T">Cualquier entidad de dominio</typeparam>
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<T?> GetByIdAsync<TId>(TId Id);
        void Delete(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}