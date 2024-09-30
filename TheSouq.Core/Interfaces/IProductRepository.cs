using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSouq.Core.Enities;

namespace TheSouq.Core.Interfaces
{
	public interface IProductRepository :IBaseRepository<Product>
	{
		public Task<IEnumerable<Product>> GetProducts();
		public Task<Product> GetProduct(int id, string[] includes = null);
		public Task InsertProduct(Product product);
		public void UpdateProduct(Product product);
		public void DeleteProduct(Product product);
	}
}
