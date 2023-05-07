using CarRental.Domain.Entities;

namespace CarRental.Domain.Repositories;

public interface IBookingRepository
{
    Task<Booking> GetByNumberAsync(long bookingNumber);
    
    Task<Booking> CreateAsync(Booking entity);
    
    Task<Booking> UpdateAsync(Booking entity);
    
    Task<int> DeleteAsync(long bookingNumber, string email);
}