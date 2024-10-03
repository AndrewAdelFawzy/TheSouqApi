using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheSouq.Core.Common.DTOS;
using TheSouq.Core.Services;

namespace TheSouq.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;
	

		public AccountController(IAuthService authService) 
		{
			_authService = authService;
			
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto dto)
		{

			var result = await _authService.RegisterAsync(dto);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}

		[HttpGet("confirmEmail")]
		public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
		{
			var result = await _authService.ConfirmEmailAsync(userId,token);
			return Ok(result);
		}


		[HttpPost("changePassword")]
		[Authorize]
		public async Task<IActionResult> ChangePasswordAsync([FromForm]ChangePasswordDto dto)
		{
			var userId = User.FindFirst("uid")?.Value;

			if (userId is null)
				return Unauthorized("user not found or not authenticated");

			var result = await _authService.ChangePasswordAsync(userId, dto);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new { message = "Password change failed.", errors });
			}

			return Ok("Password changed successfully.");
		}




		[HttpPost("Login")]
		public async Task<IActionResult> LoginAsync([FromForm] LoginDto dto)
		{
			var result = await _authService.LoginAsync(dto);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}
	}
}
