using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace JuanApp.BLL.Services;

public class EmailService(IOptions<EmailSettingsDto> options) : IEmailService
{
   
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(options.Value.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        if (isHtml)
            email.Body = new TextPart(TextFormat.Html) { Text = body };
        else
            email.Body = new TextPart(TextFormat.Text) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(options.Value.SmtpServer, options.Value.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(options.Value.Username, options.Value.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}