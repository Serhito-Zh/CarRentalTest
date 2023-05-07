using CarRental.Application.Abstractions;
using CarRental.Application.Responses;
using CarRental.Application.Shared.Helpers;
using CarRental.Domain.Entities;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Repositories;
using MediatR;

namespace CarRental.Application.Entities.Booking.Commands.CreateBooking;

/// <inheritdoc />
public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDetailsShortResponse>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly INotificationSender _notificationSender;
   
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bookingRepository"></param>
    /// <param name="notificationSender"></param>
    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository, 
        INotificationSender notificationSender)
    {
        _bookingRepository = bookingRepository;
        _notificationSender = notificationSender;
    }
    
    /// <summary>
    /// Create Booking Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BookingDetailsShortResponse> Handle(
        CreateBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Booking
        {
            Id = Guid.NewGuid(),
            PickUpDate = request.pickUpDate,
            DropOffDate = request.dropOffDate,
            CustomerEmail = request.email
        };

        var result = await _bookingRepository.CreateAsync(entity);

        if (result is null)
        {
            throw new BookingNotCreatedException();
        }
       
        var durationInHours = DateTimeHelper.GetHoursBetweenTwoDates(request.pickUpDate, request.dropOffDate);
        var durationInDays = DateTimeHelper.GetDaysFromHours(durationInHours);
        var bookingPrice = CalculationsHelper.CalculatePrice(durationInDays, durationInHours);

        var response = new BookingDetailsShortResponse(result.Number, (int)durationInHours, bookingPrice);

        var notification = new Notification()
       {
           Recipient = request.email,
           Topic = "CarRental notification",
           Message = $"Your booking number: { response.BookingNumber } is created successfully!\n" 
                            + $" Don't forget your booking duration is { response.BookingDuration } "
                            + $"and price is { response.Price }\n" 
                            + " Thank you for choosing us!"
       };

       await _notificationSender.SendAsync(notification);
       
       return response;
    }
}