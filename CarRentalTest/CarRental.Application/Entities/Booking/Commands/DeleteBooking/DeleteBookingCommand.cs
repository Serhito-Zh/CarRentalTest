using MediatR;

namespace CarRental.Application.Entities.Booking.Commands.DeleteBooking;

/// <summary>
/// Delete Booking entity command
/// </summary>
/// <param name="bookingNumber"></param>
/// <param name="email"></param>
public sealed record DeleteBookingCommand(long bookingNumber, string email) : IRequest<int>;//TODO: Success / Error instead of int
