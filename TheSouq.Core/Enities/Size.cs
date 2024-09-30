using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Enities
{
	[Index(nameof(Name),IsUnique =true)]
	public class Size
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte Id { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
