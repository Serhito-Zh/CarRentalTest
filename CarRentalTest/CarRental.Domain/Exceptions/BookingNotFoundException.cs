namespace CarRental.Domain.Exceptions;

public sealed class BookingNotFoundException : Exception
{
    public BookingNotFoundException(string number) 
        : base($"Booking with number { number } not found!") 
    { }
}