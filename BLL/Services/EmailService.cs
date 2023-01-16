using Common.Configs;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class EmailService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly EmailConfig _emailConfig;
        public EmailService(IMemoryCache memoryCache, IOptions<EmailConfig> config)
        {
            _memoryCache = memoryCache;
            _emailConfig = config.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string text)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ezdomawka", _emailConfig.Name));
            emailMessage.To.Add(new MailboxAddress("", email));
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

        public async Task SendConfirmCodeToEmailAsync(string email, string webRootPath)
        {
            Random rnd = new Random();
            int codeToConfirmEmail = rnd.Next(100000, 999999);
            var PathToTemplate = Path.Combine(webRootPath,"templates","confirmMail.html");
            var subject = "Подтверждение почты на сайте ezdomawka.com";
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
               string HtmlBody = sr.ReadToEnd();
               string messageBody = string.Format(HtmlBody, codeToConfirmEmail);
               await SendEmailAsync(email, subject, messageBody);
            }
            _memoryCache.Set(email, codeToConfirmEmail,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        public bool CheckCorrectConfirmCode(string email, string code)
        {
           if( _memoryCache.TryGetValue(email, out object? value)) return code == value!.ToString();
           return false;
        }

        public bool CheckCorrectLink(string code, out string? email)
        {
            var exist = _memoryCache.TryGetValue(code, out object? value);
            if (exist) email = value!.ToString();
            else email = null;
            return exist;
        }

        public async Task SendChangePasswordLinkToEmail(string email, string urlToMethod, string webRootPath)
        {
            var PathToTemplate = Path.Combine(webRootPath, "templates", "changePassword.html");
            var subject = "Смена пароля на сайте ezdomawka.com";
            var code = Guid.NewGuid();
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                string HtmlBody = sr.ReadToEnd();
                string messageBody = string.Format(HtmlBody, urlToMethod + "?code=" + code);
                await SendEmailAsync(email, subject, messageBody);
            }
            _memoryCache.Set(code.ToString(), email,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
        }
    }
}
