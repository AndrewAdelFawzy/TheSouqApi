using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSouq.Core.Consts
{
	public static class RegexPatterns
	{
		public const string PasswordRegex = "(?=(.*[0-9]))((?=.*[A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z]))^.{8,}$";
		public const string UserNameRegex = "^[a-zA-Z0-9-.@+]*$";
		public const string CharactersOnlyEnglish = "^[a-zA-Z-_ ]*$";
		public const string MobileNumber = "^01[0,1,2,5]{1}[0-9]{8}$";
		public const string NationalId = "^[23]\\d{13}$";

	}
}
