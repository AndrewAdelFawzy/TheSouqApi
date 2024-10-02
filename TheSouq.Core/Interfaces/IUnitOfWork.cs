using TheSouq.Core.Enities;

namespace TheSouq.Core.Interfaces
{
	public interface IUnitOfWork:IDisposable
	{
		IBaseRepository<Product> Products { get; }
		//IProductRepository Products { get; }

		int Commit();

	}
}
