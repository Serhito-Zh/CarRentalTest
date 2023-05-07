using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Customer entity
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// LastName
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Rating
    /// </summary>
    public Rating Rating { get; set; }
}