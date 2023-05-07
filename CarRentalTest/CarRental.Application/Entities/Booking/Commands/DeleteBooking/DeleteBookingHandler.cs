using CarRental.Application.Abstractions;
using CarRental.Application.Exceptions;
using CarRental.Domain.Repositories;
using MediatR;

namespace CarRental.Application.Entities.Booking.Commands.DeleteBooking;

/// <inheritdoc />
public class DeleteBookingHandler: IRequestHandler<DeleteBookingCommand, int>
{
    private readonly IBookingRepository _bookingRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bookingRepository"></param>
    public DeleteBookingHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    
    /// <summary>
    /// Delete Booking Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(
        DeleteBookingCommand request, 
        CancellationToken cancellationToken)
        => await _bookingRepository.DeleteAsync(request.bookingNumber, request.email);

}