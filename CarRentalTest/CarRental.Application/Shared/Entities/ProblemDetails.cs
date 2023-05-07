namespace CarRental.Application.Shared.Entities;

/// <summary>
/// ProblemDetails
/// </summary>
class ProblemDetails
{
    /// <summary>
    /// Status code
    /// </summary>
    public int Status { get; set; }
        
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Property name
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// List of error messages
    /// </summary>
    public string[] ErrorMessages { get; set; }
}