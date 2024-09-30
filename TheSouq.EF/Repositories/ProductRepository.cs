using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheSouq.Core.Enities;
using TheSouq.Core.Interfaces;

namespace TheSouq.EF.Repositories
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(ApplicationDbContext context) : base(context)
		{
		}

		public void DeleteProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public async Task<Product> GetProduct(int id, string[] includes = null)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> GetProducts()
		{
			throw new NotImplementedException();
		}

		public Task InsertProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public void UpdateProduct(Product product)
		{
			throw new NotImplementedException();
		}
	}
}
