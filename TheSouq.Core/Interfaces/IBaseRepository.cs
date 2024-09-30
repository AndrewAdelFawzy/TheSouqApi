using System.Linq.Expressions;
using TheSouq.Core.Consts;

namespace TheSouq.Core.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		T GetById(int id);
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
		Task<T> AddAsync(T entity);
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
		Task<T> UpdateAsync(T entity);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
		int Count();
		int Count(Expression<Func<T, bool>> criteria);
		Task<int> CountAsync();
		Task<int> CountAsync(Expression<Func<T, bool>> criteria);


	}
}
