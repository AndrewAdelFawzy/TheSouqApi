using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSouq.Core.Enities
{
	[Index(nameof(Name),IsUnique =true)]
	public class Color
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte Id { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
