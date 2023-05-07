using System.Net;
using System.Net.Mail;
using CarRental.Application.Abstractions;
using CarRental.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CarRental.Infrastructure.Services.EmailService;

/// <summary>
/// Email sender service
/// </summary>
public class EmailService : INotificationSender
{
    /// <summary>
    /// Smtp client settings
    /// </summary>
    private readonly MailSenderSettings _MailSenderSettings;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options">Mail sender</param>
    public EmailService(IOptions<MailSenderSettings> options)
        => _MailSenderSettings = options.Value;
    
    public async Task SendAsync(Notification notification)
    {
        var mail = new MailMessage();
        
        mail.To.Add(notification.Recipient);
        mail.Subject = notification.Topic;
        mail.Body = notification.Message;

        using SmtpClient smtp = new(
            _MailSenderSettings.SmtpClientUrl,
            _MailSenderSettings.SmtpClientPort)
        {
            Credentials = new NetworkCredential(
                _MailSenderSettings.SmtpClientSender,
                _MailSenderSettings.SmtpClientPassword),
            EnableSsl = _MailSenderSettings.SmtpClientEnableSSL
        };

        try
        {
            //Without correct SMTP settings we can't send, comment this for a while 
            //await smtp.SendMailAsync(mail);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"Failed sending via smtp {nameof(EmailService)}" + $"Exception: { ex }");
        }
    }
}