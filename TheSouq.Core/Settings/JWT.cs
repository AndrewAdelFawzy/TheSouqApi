using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Settings
{
	public class JWT
	{
		public string Key { get; set; } = null!;
		public string Issuer { get; set; } = null!;
		public string Audience { get; set; } = null!;
		public double DurationInDays { get; set; } 
	}
}
