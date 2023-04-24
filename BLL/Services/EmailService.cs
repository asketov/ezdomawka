using BLL.Models.Auth;
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

        public async Task SendRegisterFinishCodeToEmailAsync(RegisterModel registerModel, string webRootPath, IMailConfirmRegistrationUriGenerator registrationUriGenerator)
        {
            var codeToRegisterEmail = Guid.NewGuid();
            var PathToTemplate = Path.Combine(webRootPath,"templates","confirmMail.html");
            var subject = "Подтверждение почты на сайте ezvuz.ru";

            var redirectToRegisterLink = registrationUriGenerator.GenerateUri(codeToRegisterEmail);
            
            using (StreamReader sr = File.OpenText(PathToTemplate))
            {
               string HtmlBody = sr.ReadToEnd();
               string messageBody = string.Format(HtmlBody, redirectToRegisterLink);
               await SendEmailAsync(registerModel.Email, subject, messageBody, redirectToRegisterLink.ToString());
            }
            
            CacheData(codeToRegisterEmail.ToString(),  registerModel, 5);
        }

        public RegisterModel? TryGetRegisterFinishModel(Guid confirmCode)
        {
            if (TryUnCacheData(confirmCode.ToString(), out RegisterModel registerModel) == false)
                return null;

            return registerModel;
        }

        public async Task SendChangePasswordLinkToEmail(string email, string urlToMethod, string webRootPath)
        {
            var PathToTemplate = Path.Combine(webRootPath, "templates", "changePassword.html");
            var subject = "Смена пароля на сайте ezvuz.ru";
            var code = Guid.NewGuid();
            
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                string HtmlBody = sr.ReadToEnd();
                string messageBody = string.Format(HtmlBody, urlToMethod + "?code=" + code);
                await SendEmailAsync(email, subject, messageBody, urlToMethod);
            }

            CacheData(code.ToString(), email, 30);
        }
        public bool CheckCorrectLink(string code, out string? email)
        {
            var exist = _memoryCache.TryGetValue(code, out object? value);
            if (exist) email = value!.ToString();
            else email = null;
            return exist;
        }
        
        
        private async Task SendEmailAsync(string email, string subject, string text, string? link)
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
        
        private void CacheData<T>(string key, T value, int minutesToAutoClean)
        {
            if (minutesToAutoClean <= 0)
                throw new ArgumentException();
            
            _memoryCache.Set(key, value,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesToAutoClean)));
        }

        private bool TryUnCacheData<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
        
        public interface IMailConfirmRegistrationUriGenerator
        {
            Uri GenerateUri(Guid confirmCode);
        }

        
    }
}
