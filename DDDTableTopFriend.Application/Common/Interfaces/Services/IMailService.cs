namespace DDDTableTopFriend.Application.Common.Interfaces.Services;

public interface IMailService
{
    Task<string> SendMail(string from, string to, string subject, string body);
}
