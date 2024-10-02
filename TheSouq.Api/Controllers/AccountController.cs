using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using TheSouq.Core.Common.DTOS;
using TheSouq.Core.Services;

namespace TheSouq.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IEmailSender _emailSender;

		public AccountController(IAuthService authService, IEmailSender emailSender) 
		{
			_authService = authService;
			_emailSender = emailSender;
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
