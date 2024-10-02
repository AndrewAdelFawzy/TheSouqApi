using TheSouq.Core.Enities;
using TheSouq.Core.Interfaces;
using TheSouq.EF.Repositories;

namespace TheSouq.EF
{
	public class UnitOfWork :IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IBaseRepository<Product> Products { get; private set; }

		//public IProductRepository Products { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;	
			Products = new BaseRepository<Product>(_context);
		}

		public int Commit()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
