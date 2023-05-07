using CarRental.Domain.Entities;
using CarRental.Domain.Repositories;
using Dapper;

namespace CarRental.Infrastructure.Persistence.Repositories;

/// <summary>
/// Booking repository
/// </summary>
public class BookingRepository : IBookingRepository
{
    private readonly DbContext _context;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context"></param>
    public BookingRepository(DbContext context) => _context = context;
    
    /// <summary>
    /// Get Booking entity by number
    /// </summary>
    /// <param name="bookingNumber"></param>
    /// <returns></returns>
    public async Task<Booking> GetByNumberAsync(long bookingNumber)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            number = bookingNumber
        };
        
        var query = @"
            SELECT Number, PickUpDate, DropOffDate 
            FROM Bookings
            WHERE Number = @number
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Booking>(query, parameters);
    }

    /// <summary>
    /// Create Booking entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Booking> CreateAsync(Booking entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            guid = entity.Id,
            email = entity.CustomerEmail,
            pickUpDate = entity.PickUpDate, 
            dropOffDate = entity.DropOffDate
        };
        
        var query = @"
            INSERT INTO Bookings(Id, CustomerEmail, CarId, PickUpDate, DropOffDate)
            VALUES(@guid, @email, NULL, @pickUpDate, @dropOffDate)
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Booking>(query, parameters);
    }
    
    /// <summary>
    /// Update Booking entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Booking> UpdateAsync(Booking entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            email = entity.CustomerEmail,
            pickUpDate = entity.PickUpDate, 
            dropOffDate = entity.DropOffDate
        };

        var query = @"
            UPDATE Bookings
            SET PickUpDate = @pickUpDate,
                DropOffDate = @dropOffDate
            WHERE CustomerEmail = @email
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Booking>(query, parameters);
    }

    /// <summary>
    /// Delete Booking entity
    /// </summary>
    /// <param name="bookingNumber"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(long bookingNumber, string email)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            number = bookingNumber,
            email = email
        };
        
        var query = @"
            DELETE FROM Bookings
            WHERE Number = @number AND CustomerEmail = @email
        ";
        
        return await connection.ExecuteAsync(query, parameters);
    }
}