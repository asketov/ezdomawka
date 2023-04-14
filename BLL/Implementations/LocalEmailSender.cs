using BLL.Interfaces;

namespace BLL.Implementations;

public class LocalEmailSender : IEmailSender
{
    private readonly Action<string> _writeLocalMessage;

    public LocalEmailSender(Action<string> writeLocalMessage)
    {
        _writeLocalMessage = writeLocalMessage;
    }

    public Task SendEmailAsync(string email, string subject, string text, string? link)
    {
        _writeLocalMessage.Invoke($"Email: {email}" +
                                  $"\nLink: {link ?? "None"}" +
                                  $"\nSubject: {subject}");
        
        return Task.CompletedTask;
    }
}