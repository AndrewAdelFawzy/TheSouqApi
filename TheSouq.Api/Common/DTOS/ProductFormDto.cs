using System.ComponentModel.DataAnnotations;

namespace TheSouq.Api.Common.DTOS
{
	public class ProductFormDto
	{
		[MaxLength(500)]
		public string Title { get; set; } = null!;
		public IFormFile? Image { get; set; }
		public double Price { get; set; }

		[MaxLength(500)]
		public string Description { get; set; } = null!;
		public byte SizeId { get; set; } 
		public byte ColorId { get; set; } 
		public byte CategoryId { get; set; }

	}
}
