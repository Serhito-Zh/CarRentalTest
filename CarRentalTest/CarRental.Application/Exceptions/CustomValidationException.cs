using System.Net;
using System.Text.Json;
using CarRental.Application.Shared.Entities;

namespace CarRental.Application.Exceptions;

/// <summary>
/// CustomValidationException
/// </summary>
public sealed class CustomValidationException : Exception
{
    /// <inheritdoc />
    public CustomValidationException(Dictionary<string, string[]> errors) : base(CreateErrorMessage(errors))
    {
    }
    
    public override string StackTrace => "";
    
    private static string CreateErrorMessage(Dictionary<string, string[]> errors)
    {
        var problemDetails = errors.Select(
            error => 
            new ProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation failed",
                PropertyName = error.Key, 
                ErrorMessages = error.Value
            }).ToList();

        return JsonSerializer.Serialize(problemDetails);
    }
}