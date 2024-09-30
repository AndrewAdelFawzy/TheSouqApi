namespace TheSouq.Core.Consts
{
	public class Errors
	{
		public const string RequiredField = "This field is required";
		public const string MaxLength = "Length cant be more than {1} characters";
		public const string MaxMinLength = "{0} Length cant be more than {1} characters and less than {2}";
		public const string Dublicated = "{0} with the same value is already exists";
		public const string ExtntionNotAllowed = "This extention is not allowed ";
		public const string Filesize = "file size should be maximam 2 MB";
		public const string NumberRange = "{0} Should be from {1} to {2}";
		public const string ConfirmPasswordNotMatch = "The Password and confirm password not match !";
		public const string WeakPassword = "Password should have 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character and be at least 8 characters long";
		public const string AlphaNumric = "{0} should be letters , numbers ,(_,-,+,@) ";
		public const string EnglishCharacters = "{0} should be English letters";
		public const string InvalidPhoneNumber = "Invalid Phone number";
		public const string InvalidNationalID = "National Id should be 14 digit and starts with 2 or 3";
		public const string NotAllowedDate = "this date not allowed";
		public const string EmptyImage = "Please select an image";
	}
}
