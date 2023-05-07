using CarRental.Application.Abstractions;
using CarRental.Application.Responses;
using CarRental.Application.Shared.Helpers;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Repositories;
using MediatR;

namespace CarRental.Application.Entities.Booking.Queries;

/// <inheritdoc />
public sealed class GetBookingHandler : IRequestHandler<GetBookingByNumberQuery, BookingDetailsFullResponse>
{
    private readonly IBookingRepository _bookingRepository;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bookingRepository"></param>
    public GetBookingHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    
    /// <summary>
    /// GetBookingByNumber Handler
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BookingDetailsFullResponse> Handle(
        GetBookingByNumberQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await _bookingRepository.GetByNumberAsync(query.number);

        if (result is null)
        {
            throw new BookingNotFoundException(query.number.ToString());
        }
       
        var durationInHours = DateTimeHelper.GetHoursBetweenTwoDates(result.PickUpDate, result.DropOffDate);
        var durationInDays = DateTimeHelper.GetDaysFromHours(durationInHours);
        var bookingPrice = CalculationsHelper.CalculatePrice(durationInDays, durationInHours);

        var response = new BookingDetailsFullResponse(
            result.Number,
            result.PickUpDate, 
            result.DropOffDate, 
            (int)durationInHours, 
            bookingPrice);
        
        return response;
    }
}