using TheSouq.Core.Enities;
using TheSouq.Core.Interfaces;

namespace TheSouq.Api
{
	public interface IUnitOfWork:IDisposable
	{
		IBaseRepository<Product> Products { get; }
		//IProductRepository Products { get; }

		int Commit();

	}
}
