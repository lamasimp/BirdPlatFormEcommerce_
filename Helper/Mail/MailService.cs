using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BirdPlatFormEcommerce.Helper.Mail
{
    public class MailService : IMailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public MailService(IConfiguration configuration)
        {
            _host = configuration["MailSettings:Host"];
            _port = configuration["MailSettings:From"] != null ? int.Parse(configuration["MailSettings:From"]) : 587 ;
            _username = configuration["MailSettings:Mail"];
            _password = configuration["MailSettings:Password"];
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_username);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
           
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_username, _password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
