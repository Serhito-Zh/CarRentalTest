using CarRental.Application.Responses;
using CarRental.Domain.Repositories;
using MediatR;

namespace CarRental.Application.Entities.Report.Queries;

/// <inheritdoc />
public sealed class GetReportByDateRangeHandler : IRequestHandler<GetReportByDateRangeQuery, List<ReportResponse>>
{
    private readonly IReportRepository _reportRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="reportRepository"></param>
    public GetReportByDateRangeHandler(IReportRepository reportRepository)
    => _reportRepository = reportRepository;

    
    public async Task<List<ReportResponse>> Handle(
        GetReportByDateRangeQuery request, 
        CancellationToken cancellationToken)
    {
        var bookings = await _reportRepository.FindBookingsByRange(
            request.startDate, 
            request.endDate,
            request.settigns);

        return bookings.Select(
            booking => 
                new ReportResponse(booking.Number, booking.PickUpDate)).ToList();
    }
}