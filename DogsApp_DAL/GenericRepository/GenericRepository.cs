using DogsApp_DAL.Helpers;
using DogsApp_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DogsApp_DAL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        protected readonly EfDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        private readonly ISortHelper<T> _sortHelper;

        public GenericRepository(EfDbContext dbContext, ISortHelper<T> sortHelper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _sortHelper = sortHelper;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetByPredicate(Expression<Func<T, bool>> expression)
            => _dbSet.Where(expression);
            
        public async Task<IEnumerable<T>> GetAllAsync(QueryStringParameters queryParameters)
        {
            var orederedByParameter = _sortHelper.ApplySort(_dbSet.AsNoTracking(), queryParameters.Attribute!, queryParameters.Order!);
            var result = await orederedByParameter.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();
            return result;
        }
    }
}
