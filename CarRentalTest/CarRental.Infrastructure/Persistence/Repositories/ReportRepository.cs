using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Domain.Repositories;
using Dapper;

namespace CarRental.Infrastructure.Persistence.Repositories;

/// <summary>
/// Report repository
/// </summary>
public class ReportRepository : IReportRepository
{
    private readonly DbContext _context;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context"></param>
    public ReportRepository(DbContext context) => _context = context;

    /// <summary>
    /// Find all bookings between two date 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Booking>> FindBookingsByRange(
        DateTime startDate, 
        DateTime endDate, 
        ReportSettings settings)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            startDate = startDate,
            endDate = endDate
        };

        var orderDirection = settings.order == Order.ASC ? "ASC" : "DESC";

        var query = $"\n" +
                    $" SELECT Number, PickUpDate \n" +
                    $" FROM Bookings\n " +
                    $" WHERE PickUpDate >= @startDate and DropOffDate <= @endDate\n" +
                    $" ORDER BY Number { orderDirection }\n" +
                    $" LIMIT { (int)settings.limit }, { (int)settings.offset }\n";
        
        return await connection.QueryAsync<Booking>(query, parameters);
    }
}