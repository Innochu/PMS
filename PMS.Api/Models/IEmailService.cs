namespace PMS.Api.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync8(string toEmail, string subject, string htmlBody);
        Task SendEmailAsync(string toEmail, string subject, string htmlBody, CancellationToken cancellationToken = default);
    }
}