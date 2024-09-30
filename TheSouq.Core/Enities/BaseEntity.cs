using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Enities
{
	public class BaseEntity
	{
		public bool IsDeleted { get; set; }

		//public string? CreatedById { get; set; }
		//public ApplicationUser? CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		//public string? UpdatedById { get; set; }
		//public ApplicationUser? UpdatedBy { get; set; }


		public DateTime? UpdatedAt { get; set; }
	}
}
