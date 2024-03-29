using TableTopFriend.Application.Common.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace TableTopFriend.Infrastructure.Services.Mail;

public class MailService : IMailService
{
    private readonly MailServiceSettings _mailServiceSettings;
    public MailService(IOptions<MailServiceSettings> mailServiceSettings)
    {
        _mailServiceSettings = mailServiceSettings.Value;
    }

    public async Task<string> SendMail(
        string to,
        string subject,
        string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_mailServiceSettings.Username));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _mailServiceSettings.Host,
            _mailServiceSettings.Port,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _mailServiceSettings.Username,
            _mailServiceSettings.Password);

        var response = await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        return response;
    }

    public async Task<string> SendConfirmationMail(string to, string confirmationLink)
    {
        return await SendMail(
            to,
            "Table Top Friend confirmation email",
            $"Welcome to Table Top Friend here`s your confirmation link {confirmationLink}");
    }
}
