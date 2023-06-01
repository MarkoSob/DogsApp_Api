using DogsApp_DAL.Models;
using System.Linq.Expressions;

namespace DogsApp_DAL.GenericRepository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(QueryStringParameters queryParameters);
        IQueryable<T> GetByPredicate(Expression<Func<T, bool>> expression);
    }
}