using Microsoft.Extensions.Logging;
using PMS.Api.Services;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        using var client = new SmtpClient("smtp.yourprovider.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("your-email@domain.com", "your-smtp-password"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage("your-email@domain.com", to, subject, htmlBody)
        {
            IsBodyHtml = true
        };

        try
        {
            await client.SendMailAsync(mailMessage);
        }
        catch (SmtpFailedRecipientException ex)
        {
            _logger.LogError(ex, "SMTP failed recipient error: {Recipient}", ex.FailedRecipient);
            throw; // rethrow if needed
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP exception: StatusCode={StatusCode}, Message={Message}", ex.StatusCode, ex.Message);
            throw; // rethrow if needed
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "General email sending error");
            throw;
        }
    }
}
