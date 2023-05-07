using CarRental.Application.Responses;
using MediatR;

namespace CarRental.Application.Entities.Booking.Commands.CreateBooking;

/// <summary>
/// Create Booking entity command
/// </summary>
/// <param name="pickUpDate"></param>
/// <param name="dropOffDate"></param>
/// <param name="email">customer email</param>
public sealed record CreateBookingCommand(
    DateTime pickUpDate, 
    DateTime dropOffDate, 
    string email) : IRequest<BookingDetailsShortResponse>
{ }