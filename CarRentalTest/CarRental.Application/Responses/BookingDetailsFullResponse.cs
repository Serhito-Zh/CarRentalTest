namespace CarRental.Application.Responses;

/// <summary>
/// Booking response
/// </summary>
public class BookingDetailsFullResponse : BookingDetailsShortResponse
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="price"></param>
    /// <param name="pickUpDate"></param>
    /// <param name="dropOffDate"></param>
    /// <param name="bookingNumber"></param>
    /// <param name="bookingDuration"></param>
    public BookingDetailsFullResponse(
        long bookingNumber,
        DateTime pickUpDate, 
        DateTime dropOffDate,
        int bookingDuration, 
        int price) : base(bookingNumber, bookingDuration, price)
    {
        PickUpDate = pickUpDate;
        DropOffDate = dropOffDate;
    }

    /// <summary>
    /// PickUpDate
    /// </summary>
    public DateTime PickUpDate { get; init; }
    
    /// <summary>
    /// DropOffDate
    /// </summary>
    public DateTime DropOffDate { get; init; }
}