namespace CarRental.Infrastructure.Services.EmailService;

/// <summary>
/// Mail sender settings
/// </summary>
public class MailSenderSettings
{
    /// <summary>
    /// Sender email
    /// </summary>
    public string SmtpClientSender { get; init; } = default!;

    /// <summary>
    /// Sender email password
    /// </summary>
    public string SmtpClientPassword { get; init; } = default!;

    /// <summary>
    /// Smtp server url
    /// </summary>
    public string SmtpClientUrl { get; init; } = default!;

    /// <summary>
    /// Smtp port
    /// </summary>
    public int SmtpClientPort { get; init; } = default!;

    /// <summary>
    /// Smtp SSL port
    /// </summary>
    public int SmtpClientSSLPort { get; init; } = default!;

    /// <summary>
    /// Enable/Disable SSL
    /// </summary>
    public bool SmtpClientEnableSSL { get; init; } = default!;
}