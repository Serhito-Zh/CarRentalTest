namespace CarRental.Domain.Entities;

/// <summary>
/// Location
/// </summary>
public class Location : BaseEntity
{
    /// <summary>
    /// State
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// City
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Street
    /// </summary>
    public string Street { get; set; }
    
    /// <summary>
    /// Coordinate (latitude)
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Coordinate (longitude)
    /// </summary>
    public double Longitude { get; set; }
}

