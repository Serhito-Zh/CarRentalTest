using System.Net;
using CarRental.Application.Entities.Report.Queries;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;

/// <summary>
/// Report controller
/// </summary>
[Route("/report")]
[AllowAnonymous]
public sealed class ReportController : ApiController
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="sender"></param>
    public ReportController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Get info about bookings by date range with settings (filtering/sorting/paging)
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="order"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(object[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetReportByDateRangeAsync(
        DateTime startDate, 
        DateTime endDate,
        int? limit,
        int? offset,
        int? order,
        CancellationToken cancellationToken)
    {
        var query = new GetReportByDateRangeQuery(
            startDate, 
            endDate, 
            new ReportSettings(
                (PageSize)limit,
                (PageSize)offset,
                (Order)order));
        
        var result = await Sender.Send(query, cancellationToken);
        
        return Ok(result);
    }
}