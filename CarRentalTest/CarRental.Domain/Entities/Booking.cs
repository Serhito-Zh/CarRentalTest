namespace CarRental.Domain.Entities;

/// <summary>
/// Booking entity
/// </summary>
public sealed class Booking : BaseEntity
{
    /// <summary>
    /// Booking number
    /// </summary>
    public long Number { get; set; }

    /// <summary>
    /// Pickup date
    /// </summary>
    public DateTime PickUpDate { get; set; }

    /// <summary>
    /// Drop off date
    /// </summary>
    public DateTime DropOffDate { get; set; }
    
    /// <summary>
    /// Customer email
    /// </summary>
    public string CustomerEmail { get; set; }
}