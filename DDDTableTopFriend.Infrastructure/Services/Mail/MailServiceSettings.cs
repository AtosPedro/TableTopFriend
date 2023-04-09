namespace  DDDTableTopFriend.Infrastructure.Services.Mail;

public class MailServiceSettings
{
    public const string SectionName = "MailServiceSettings";
    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Port { get; set; }
}
