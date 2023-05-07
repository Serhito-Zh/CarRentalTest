using CarRental.Application.Responses;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Entities.Report.Queries;

/// <summary>
/// Get Report by date/time range query
/// </summary>
/// <param name="startDate"></param>
/// <param name="endDate"></param>
public sealed record GetReportByDateRangeQuery(
    DateTime startDate, 
    DateTime endDate, 
    ReportSettings settigns) 
    : IRequest<List<ReportResponse>>
{ }