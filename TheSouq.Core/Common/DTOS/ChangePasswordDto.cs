using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Common.DTOS
{
	public class ChangePasswordDto
	{
		public string CurrentPassword { get; set; } = null!;
		public string NewPassword { get; set; } = null!;
	}
}
