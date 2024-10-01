using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Enities
{
	[Index(nameof(UserName), IsUnique = true)]
	[Index(nameof(Email), IsUnique = true)]
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; } = null!;
		public bool IsDeleted { get; set; }
		public string? CreatedById { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public string? UpdatedById { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
