namespace CarRental.Domain.Entities;

/// <summary>
/// Notification message
/// </summary>
public class Notification
{
    /// <summary>
    /// Recipient
    /// </summary>
    public string Recipient { get; set; }
    
    /// <summary>
    /// Topic
    /// </summary>
    public string Topic { get; set; }
    
    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }
}