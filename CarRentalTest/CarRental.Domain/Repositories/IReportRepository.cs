using CarRental.Domain.Entities;

namespace CarRental.Domain.Repositories;

/// <summary>
/// Report repository
/// </summary>
public interface IReportRepository
{
    Task<IEnumerable<Booking>> FindBookingsByRange(DateTime startDate, DateTime endDate, ReportSettings settings);
}