using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSouq.Core.Enities
{
	[Index(nameof(Name),IsUnique = true)]
	public class Category : BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte Id { get; set; }
		public string Name { get; set; } = null!;
		public List<Product> Products { get; set; } = new List<Product>();
	}
}
