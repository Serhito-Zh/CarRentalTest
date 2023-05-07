namespace CarRental.Application.Responses;

/// <summary>
/// Booking response
/// </summary>
public class BookingDetailsShortResponse
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bookingNumber"></param>
    /// <param name="bookingDuration"></param>
    /// <param name="price"></param>
    public BookingDetailsShortResponse(long bookingNumber, int bookingDuration, int price)
    {
        BookingNumber = bookingNumber;
        BookingDuration = bookingDuration;
        Price = price;
    }

    public BookingDetailsShortResponse()
    {
    }

    /// <summary>
    /// BookingNumber
    /// </summary>
    public long BookingNumber { get; init; }

    /// <summary>
    /// BookingDuration
    /// </summary>
    public int BookingDuration { get; init; }

    /// <summary>
    /// Price
    /// </summary>
    public int Price { get; init; }
}