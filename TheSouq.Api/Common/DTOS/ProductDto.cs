using System.ComponentModel.DataAnnotations;
using TheSouq.Core.Enities;

namespace TheSouq.Api.Common.DTOS
{
	public class ProductDto
	{
		[MaxLength(500)]
		public string Title { get; set; } = null!;
		public double Price { get; set; }
		[MaxLength(500)]
		public string Description { get; set; } = null!;
		public string Size { get; set; } = null!;
		public string Color {  get; set; } =null!;
		public string? ImageUrl { get; set; }
		public string Category { get; set; } = null!;
	}
}
