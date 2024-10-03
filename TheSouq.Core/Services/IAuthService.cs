using Microsoft.AspNetCore.Identity;
using TheSouq.Core.Common.DTOS;

namespace TheSouq.Core.Services
{
	public interface IAuthService
	{
		Task<AuthDto> RegisterAsync(RegisterDto dto);
		Task<AuthDto> LoginAsync(LoginDto dto);
		Task<string> ConfirmEmailAsync(string Id, string Token);
		Task<IdentityResult> ChangePasswordAsync(string userId,ChangePasswordDto dto);

	}
}
