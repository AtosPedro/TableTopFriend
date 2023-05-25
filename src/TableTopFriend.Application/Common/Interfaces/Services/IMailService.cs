namespace TableTopFriend.Application.Common.Interfaces.Services;

public interface IMailService
{
    Task<string> SendMail(string to, string subject, string body);
    Task<string> SendConfirmationMail(string to, string confirmationLink);
}
