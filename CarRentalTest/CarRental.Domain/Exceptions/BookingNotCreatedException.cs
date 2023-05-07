namespace CarRental.Domain.Exceptions;

public class BookingNotCreatedException : Exception
{
    public BookingNotCreatedException()
        : base($"Failed to create Booking entity!") 
    { }
}