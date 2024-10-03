using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheSouq.Core.Common.DTOS;
using TheSouq.Core.Consts;
using TheSouq.Core.Enities;
using TheSouq.Core.Services;
using TheSouq.Core.Settings;

namespace TheSouq.EF.ServicesClass
{
	public class AuthService : IAuthService
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		private readonly JWT _jwt;

		public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_jwt = jwt.Value;
			_roleManager = roleManager;
			_emailSender = emailSender;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto dto)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });

			return await _userManager.ChangePasswordAsync(user,dto.CurrentPassword,dto.NewPassword);
		}

		public async Task<AuthDto> LoginAsync(LoginDto dto)
		{
			AuthDto AuthDto = new AuthDto();

			var user = await _userManager.FindByEmailAsync(dto.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
			{
				AuthDto.Message = "Email or Password is incorrect";
				return AuthDto;
			}

			var jwtSecurityToken = await CreateJwtToken(user);
			var rolesList = await _userManager.GetRolesAsync(user);

			AuthDto.IsAuthenticated = true;
			AuthDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			AuthDto.UserName = user.UserName;
			AuthDto.Roles = rolesList.ToList();
			AuthDto.Email = user.Email;
			AuthDto.ExpiresOn = jwtSecurityToken.ValidTo;

			return AuthDto;
		}

		public async Task<AuthDto> RegisterAsync(RegisterDto dto)
		{
			if (await _userManager.FindByEmailAsync(dto.Email) is not null)
				return new AuthDto { Message = "This email is already exist" };

			if (await _userManager.FindByNameAsync(dto.Username) is not null)
				return new AuthDto { Message = "This UserName is already exist" };

			var user = new ApplicationUser()
			{
				UserName = dto.Username,
				Email = dto.Email,
				FullName = dto.FullName
			};

			var result = await _userManager.CreateAsync(user, dto.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in result.Errors)
				{
					errors += $"{error.Description}";
				}

				return new AuthDto { Message = errors };
			}

			await _userManager.AddToRoleAsync(user, AppRoles.User);

			var jwtSecurityToken = await CreateJwtToken(user);

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			// Create the confirmation link

			var confirmationLink = $"https://localhost:44331/api/Account/confirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

			// Send the confirmation email
			await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href=\"{confirmationLink}\">link</a>");

			return new AuthDto
			{
				Email = user.Email,
				ExpiresOn = jwtSecurityToken.ValidTo,
				IsAuthenticated = true,
				Roles = new List<string> { "User" },
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				UserName = user.UserName,
				Message = "An email is send to you , please confirm your email"
			};
		}

		public async Task<string> ConfirmEmailAsync(string userId, string token)
		{

			if (userId == null || token == null)
				return "Invalid email confirmation request";

			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
				return "User not found";

			var result = await _userManager.ConfirmEmailAsync(user, token);


			return result.Succeeded ? "Email confirmed successfully!" : "Email confirmation failed";
		}



		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id),
				
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				claims: claims,
				expires: DateTime.Now.AddDays(_jwt.DurationInDays),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}

		
	}
}
