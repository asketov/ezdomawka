using BLL.Interfaces;
using Common.Configs;
using MailKit.Net.Smtp;
using MimeKit;

namespace BLL.Implementations;

public class EmailSender : IEmailSender
{
    private readonly EmailConfig _emailConfig;

    public EmailSender(EmailConfig emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(string email, string subject, string text, string? link)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("ezdomawka", _emailConfig.Name));
        emailMessage.To.Add(new MailboxAddress(email, email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = text
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync(_emailConfig.Name, _emailConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}