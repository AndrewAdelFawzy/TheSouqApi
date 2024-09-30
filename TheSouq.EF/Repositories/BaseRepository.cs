﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheSouq.Core.Consts;
using TheSouq.Core.Interfaces;

namespace TheSouq.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected ApplicationDbContext _context;

		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.SingleOrDefaultAsync(criteria);
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.Where(criteria).ToListAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
		{
			return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
		{
			IQueryable<T> query = _context.Set<T>().Where(criteria);

			if (take.HasValue)
				query = query.Take(take.Value);

			if (skip.HasValue)
				query = query.Skip(skip.Value);

			if (orderBy != null)
			{
				if (orderByDirection == OrderBy.Ascending)
					query = query.OrderBy(orderBy);
				else
					query = query.OrderByDescending(orderBy);
			}

			return await query.ToListAsync();
		}

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
			return entities;
		}

		public T Update(T entity)
		{
			_context.Update(entity);
			return entity;
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
		}


		public async Task<int> CountAsync()
		{
			return await _context.Set<T>().CountAsync();
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
		{
			return await _context.Set<T>().CountAsync(criteria);
		}

		async Task<T> IBaseRepository<T>.UpdateAsync(T entity)
		{
			 _context.Update<T>(entity);
			await _context.SaveChangesAsync();
			return  entity;
		}

		public int Count()
		{
			return _context.Set<T>().Count();
		}

		public int Count(Expression<Func<T, bool>> criteria)
		{
			return _context.Set<T>().Count(criteria);
		}
	}
}
