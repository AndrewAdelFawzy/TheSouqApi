using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSouq.Core.Consts;

namespace TheSouq.Core.Seeds
{
	public class DefaultRoles
	{
		public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any()) 
			{
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
				await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
			}
		}
	}
}
