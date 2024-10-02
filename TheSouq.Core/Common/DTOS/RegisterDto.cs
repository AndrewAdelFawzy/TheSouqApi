using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TheSouq.Core.Common.DTOS
{
	public class RegisterDto
	{
		[Required, StringLength(150)]
		public string FullName { get; set; } = null!;

		[Required, StringLength(50)]
		public string Username { get; set; } = null!;

		[Required, StringLength(128)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;

		[Required, StringLength(256)]
		public string Password { get; set; } = null!;

	}
}
