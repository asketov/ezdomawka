using BLL.Interfaces;

namespace BLL.Implementations;

public class LocalEmailSender : IEmailSender
{
    private readonly Action<string> _writeLocalMessage;

    public LocalEmailSender(Action<string> writeLocalMessage)
    {
        _writeLocalMessage = writeLocalMessage;
    }

    public Task SendEmailAsync(string email, string subject, string text)
    {
        _writeLocalMessage.Invoke($"Email: {email}" +
                                  $"\nSubject {subject}" +
                                  $"\nText {text}");
        
        return Task.CompletedTask;
    }
}