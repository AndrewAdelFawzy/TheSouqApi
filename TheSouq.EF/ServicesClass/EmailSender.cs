using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheSouq.Core.Settings;

namespace TheSouq.EF.ServicesClass
{
	public class EmailSender : IEmailSender
	{

		private readonly MailSettings _mailSettings;

		public EmailSender(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			MailMessage message = new()
			{
				From = new MailAddress(_mailSettings.Email!, _mailSettings.DisplayName),
				Body = htmlMessage,
				Subject = subject,
				IsBodyHtml = true
			};

			message.To.Add(email);

			SmtpClient smtpClient = new(_mailSettings.Host)
			{
				Port = _mailSettings.Port,
				Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
				EnableSsl = true
			};

			await smtpClient.SendMailAsync(message);
			smtpClient.Dispose();
		}
	}
}
