using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Abstractions;

/// <summary>
/// Base API controller with MediatR
/// </summary>
[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="sender"></param>
    protected ApiController(ISender sender) => Sender = sender;
}