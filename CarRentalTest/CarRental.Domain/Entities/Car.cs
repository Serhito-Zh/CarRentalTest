namespace CarRental.Domain.Entities;

/// <summary>
/// Car entity
/// </summary>
public sealed class Car : BaseEntity
{
    /// <summary>
    /// Registration number
    /// </summary>
    public string Number { get; set; }
    
    /// <summary>
    /// Model
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Brand
    /// </summary>
    public string Brand { get; set; }
    
    /// <summary>
    /// Color
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    /// Passengers capacity
    /// </summary>
    public int Capacity { get; set; }
    
    /// <summary>
    /// Customer email
    /// </summary>
    public string CustomerEmail { get; set; }
    
    /// <summary>
    /// Car location identifier
    /// </summary>
    public Guid? LocationId { get; set; }
}