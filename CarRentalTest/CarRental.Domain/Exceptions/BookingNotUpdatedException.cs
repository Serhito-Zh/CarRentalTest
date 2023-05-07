namespace CarRental.Domain.Exceptions;

public class BookingNotUpdatedException : Exception
{
    public BookingNotUpdatedException(string email)
        : base($"Failed to update Booking entity, maybe because entity with email: { email } doesn't exist!") 
    { }
}