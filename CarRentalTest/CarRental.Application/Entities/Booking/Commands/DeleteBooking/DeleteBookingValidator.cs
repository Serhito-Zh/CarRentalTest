using System.Text.RegularExpressions;
using FluentValidation;

namespace CarRental.Application.Entities.Booking.Commands.DeleteBooking;

public sealed class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingValidator()
    {
        RuleFor(x => x.bookingNumber).NotEmpty();

        RuleFor(x => x.email).NotEmpty().EmailAddress();
    }
}