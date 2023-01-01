using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;

namespace BLL.Services
{
    public class EmailService
    {
        private readonly IMemoryCache _memoryCache;
        public EmailService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public static async Task SendEmailAsync(string email, string subject, string text)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ezdomawka", "ezdomawka@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("ezdomawka@gmail.com", "fdviusqezjykwsee");
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

        //public async Task SendRecoverCodeToEmailAsync(string email, string WebRootPath)
        //{
        //    var PathToTemplate = WebRootPath + Path.DirectorySeparatorChar.ToString() + "templates"
        //                         + Path.DirectorySeparatorChar.ToString() +
        //                         "RecoverPassword.html";
        //    var subject = "Восстановление пароля на сайте SweetsShopNCH";
        //    string HtmlBody = "";
        //    using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
        //    {
        //        HtmlBody = sr.ReadToEnd();
        //    }
        //    Random rnd = new Random();
        //    CodeToRecoverPassword = rnd.Next(100000, 999999);
        //    string messageBody = string.Format(HtmlBody, CodeToRecoverPassword);
        //    await SendEmailAsync(email, subject, messageBody);
        //}
    }
}
