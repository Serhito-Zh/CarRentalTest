using CarRental.Application.Responses;
using MediatR;

namespace CarRental.Application.Entities.Booking.Queries;

/// <summary>
/// Get booking by number query
/// </summary>
/// <param name="number">booking number</param>
public sealed record GetBookingByNumberQuery(long number) : IRequest<BookingDetailsFullResponse> { }