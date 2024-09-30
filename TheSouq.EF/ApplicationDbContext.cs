using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSouq.Core.Enities;

namespace TheSouq.EF
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Size> Sizes { get; set; }
	}
}
