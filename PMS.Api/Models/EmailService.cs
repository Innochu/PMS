using System.Net;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PMS.Api.Models;
using System.Threading;

namespace PMS.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }


        public async Task SendEmailAsync8(string toEmail, string subject, string htmlBody)
        {
            var apiKey = Environment.GetEnvironmentVariable("BREVO_API_KEY");
            //var api = _settings.BREVO_API_KEY;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", apiKey);

            var payload = new
            {
                sender = new { name = "IDPMS System", email = "chuksinnocent1@gmail.com" },
                to = new[] { new { email = toEmail } },
                subject = subject,
                htmlContent = htmlBody
            };

            var response = await client.PostAsJsonAsync("https://api.brevo.com/v3/smtp/email", payload);
            response.EnsureSuccessStatusCode();
        }



        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, CancellationToken cancellationToken = default)
        {
            // Pre-resolve SMTP host so DNS failures are visible and descriptive
            try
            {
                var addresses = await Dns.GetHostAddressesAsync(_settings.Host, cancellationToken);
                if (addresses == null || addresses.Length == 0)
                    throw new InvalidOperationException($"DNS lookup returned no addresses for '{_settings.Host}'.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to resolve SMTP host '{_settings.Host}': {ex.Message}", ex);
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var body = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = body.ToMessageBody();

            using var client = new SmtpClient();

            SecureSocketOptions socketOptions = _settings.UseSsl
                ? SecureSocketOptions.SslOnConnect
                : _settings.UseStartTls
                    ? SecureSocketOptions.StartTls
                    : SecureSocketOptions.None;

            await client.ConnectAsync(_settings.Host, _settings.Port, socketOptions, cancellationToken);

            if (!string.IsNullOrWhiteSpace(_settings.Username))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            }

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}