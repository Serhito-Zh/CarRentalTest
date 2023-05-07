using CarRental.Application.Abstractions;
using CarRental.Application.Responses;
using CarRental.Application.Shared.Helpers;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Repositories;
using MediatR;

namespace CarRental.Application.Entities.Booking.Commands.UpdateBooking;

/// <inheritdoc />
public sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDetailsShortResponse>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly INotificationSender _notificationSender;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bookingRepository"></param>
    /// <param name="notificationSender"></param>
    public UpdateBookingCommandHandler(
        IBookingRepository bookingRepository, 
        INotificationSender notificationSender)
    {
        _bookingRepository = bookingRepository;
        _notificationSender = notificationSender;
    }
    
    /// <summary>
    /// Update Booking Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BookingDetailsShortResponse> Handle(
        UpdateBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Booking
        {
            PickUpDate = request.pickUpDate,
            DropOffDate = request.dropOffDate,
            CustomerEmail = request.email
        };

       var result = await _bookingRepository.UpdateAsync(entity);
       
       if (result is null)
       {
           throw new BookingNotUpdatedException(request.email);
       }
       
       var durationInHours = DateTimeHelper.GetHoursBetweenTwoDates(request.pickUpDate, request.dropOffDate);
       var durationInDays = DateTimeHelper.GetDaysFromHours(durationInHours);
       var bookingPrice = CalculationsHelper.CalculatePrice(durationInDays, durationInHours);

       var response = new BookingDetailsShortResponse(result.Number, (int)durationInHours, bookingPrice);
       
       return response;
    }
}