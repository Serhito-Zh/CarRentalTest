using CarRental.Application.Entities.Booking.Commands.UpdateBooking;
using CarRental.Application.Shared;
using FluentValidation;

namespace CarRental.Application.Entities.Booking.Queries;

public class GetBookingValidator : AbstractValidator<GetBookingByNumberQuery>
{
    public GetBookingValidator()
    {
        RuleFor(x => x.number).NotEmpty();
    }
}