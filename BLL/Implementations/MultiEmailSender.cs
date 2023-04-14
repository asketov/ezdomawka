using BLL.Interfaces;

namespace BLL.Implementations;

public class MultiEmailSender : IEmailSender
{
    private readonly IEnumerable<IEmailSender> _emailSenders;

    public MultiEmailSender(params IEmailSender[] emailSenders)
    {
        _emailSenders = emailSenders;
    }

    public async Task SendEmailAsync(string email, string subject, string text)
    {
        foreach (var emailSender in _emailSenders)
        {
           await emailSender.SendEmailAsync(email, subject, text);
        }
    }
}