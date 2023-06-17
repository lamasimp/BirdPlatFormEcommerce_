namespace BirdPlatFormEcommerce.Helper.Mail
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
