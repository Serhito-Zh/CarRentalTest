namespace CarRental.Domain.Entities;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime LastUpdateDate { get; set; }
}