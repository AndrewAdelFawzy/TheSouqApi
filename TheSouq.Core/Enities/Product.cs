using System.ComponentModel.DataAnnotations;

namespace TheSouq.Core.Enities
{
	public class Product :BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(500)]
		public string Title { get; set; } = null!;
		public double Price { get; set; }
		[MaxLength(500)]
		public string Description { get; set; } = null!;
		public byte SizeId { get; set; }
		public Size Size { get; set; } = null!;
		public byte ColorId { get; set; }
		public Color Color { get; set; } = null!;
		public string? ImageUrl { get; set; }
		public byte CategoryId { get; set; }
		public Category Category { get; set; } = null!;
	}
}
