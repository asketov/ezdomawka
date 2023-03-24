using BLL.Interfaces;
using BLL.Models.Auth;
using Common.Configs;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class EmailService
    {
        private readonly IMemoryCache _memoryCache;

        private readonly IEmailSender _emailSender;

        public EmailService(IMemoryCache memoryCache, IEmailSender emailSender)
        {
            _memoryCache = memoryCache;
            _emailSender = emailSender;
        }

        public async Task SendRegisterFinishCodeToEmailAsync(RegisterModel registerModel, string webRootPath, IMailConfirmRegistrationUriGenerator registrationUriGenerator)
        {
            var codeToRegisterEmail = Guid.NewGuid();
            var PathToTemplate = Path.Combine(webRootPath,"templates","confirmMail.html");
            var subject = "Подтверждение почты на сайте ezdomawka.com";

            var redirectToRegisterLink = registrationUriGenerator.GenerateUri(codeToRegisterEmail);
            
            using (StreamReader sr = File.OpenText(PathToTemplate))
            {
               string HtmlBody = sr.ReadToEnd();
               string messageBody = string.Format(HtmlBody, redirectToRegisterLink);
               await SendEmailAsync(registerModel.Email, subject, messageBody);
            }
            
            CacheData(codeToRegisterEmail.ToString(),  registerModel, 5);
        }

        public async Task<RegisterModel?> TryGetRegisterFinishModelAsync(Guid confirmCode)
        {
            if (TryUnCacheData(confirmCode.ToString(), out RegisterModel registerModel) == false)
                return null;

            return registerModel;
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

            CacheData(code.ToString(), email, 30);
        }
        public bool CheckCorrectLink(string code, out string? email)
        {
            var exist = _memoryCache.TryGetValue(code, out object? value);
            if (exist) email = value!.ToString();
            else email = null;
            return exist;
        }
        
        
        private async Task SendEmailAsync(string email, string subject, string text)
        {
            await _emailSender.SendEmailAsync(email, subject, text);
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
